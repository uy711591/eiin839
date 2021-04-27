using Newtonsoft.Json;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Net;
using System.ServiceModel.Web;

namespace RoutingWithBike
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service" à la fois dans le code et le fichier de configuration.
    public class Service : IService
    {

        private static string JCDECAUX_API_KEY = "0fc4cc6a2d64e2be7cd109281d1640fd628eaa55";
        private static string JCDECAUX_API_BASE_URL = "https://api.jcdecaux.com/vls/v3/";
        private static string ORS_API_KEY = "5b3ce3597851110001cf62483b538712304041e6985bf0afa64e3b46";
        private static string ORS_API_BASE_URL = "https://api.openrouteservice.org/";
        private static string WEB_PROXY_SERVICE_URL = "http://localhost:8733/Design_Time_Addresses/WebProxyService/RestService/";
        private static List<Station> stations;

        public Service()
        {
            if (stations == null)
            {
                string stationsResponse = doRequest(JCDECAUX_API_BASE_URL + "stations?apiKey=" + JCDECAUX_API_KEY);
                stations = JsonConvert.DeserializeObject<List<Station>>(stationsResponse);
                System.Diagnostics.Debug.WriteLine("feteched the list of stations");
            }
        }

        private string doRequest(string uri)
        {
            string returnValue;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                returnValue = reader.ReadToEnd();
            }
            return returnValue;
        }

        private Station getClosestStation(GeoCoordinate geoCoordinate, bool deposit)
        {
            // on trie deux a deux notre liste de stations en comparant avec la distance de la coordonne de l'addresse renseignee
            stations.Sort(delegate (Station x, Station y)
            {
                GeoCoordinate coordinateX = new GeoCoordinate(x.position.latitude, x.position.longitude);
                GeoCoordinate coordinateY = new GeoCoordinate(y.position.latitude, y.position.longitude);
                return coordinateX.GetDistanceTo(geoCoordinate).CompareTo(coordinateY.GetDistanceTo(geoCoordinate));
            });

            foreach (Station station in stations)
            {
                string stationInfoResponse =  doRequest(WEB_PROXY_SERVICE_URL + "request?uri=" + JCDECAUX_API_BASE_URL + "stations/" + station.number + "?contract=" + station.contractName + "%26apiKey=" + JCDECAUX_API_KEY);
                var stationInfoResult = JsonConvert.DeserializeObject<GetResult> (stationInfoResponse);
                var stationInfo = JsonConvert.DeserializeObject<Station>(stationInfoResult.getResult);
                if ((deposit && stationInfo.mainStands.availabilities.stands > 1) ||
                    (!deposit && stationInfo.mainStands.availabilities.bikes > 1))
                { 
                    // TODO (possible extinction) : quantity management
                    // return the first available station from the sorted list depending on bikes or stands availabilities
                    return station;
                }
            }

            return null;
        }

        private RoutingWithBikeItinirary.Itinirary getItinary(GeoCoordinate a, GeoCoordinate b)
        {
            string start = a.Longitude.ToString().Replace(",", ".") + "," + a.Latitude.ToString().Replace(",", ".");
            string end = b.Longitude.ToString().Replace(",", ".") + "," + b.Latitude.ToString().Replace(",", ".");
            string itiniraryResponse = doRequest(ORS_API_BASE_URL + "v2/directions/foot-walking?api_key=" + ORS_API_KEY + "&start=" + start + "&end=" + end);
            RoutingWithBikeItinirary.Itinirary itinirary = JsonConvert.DeserializeObject<RoutingWithBikeItinirary.Itinirary>(itiniraryResponse);
            return itinirary;
        }

        public List<RoutingWithBikeItinirary.Itinirary> getClosestStations(string fromAddress, string toAddress)
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            // TODO (possible extinction) : handle address erros : features list
            string departureGeoCodeResponse = doRequest(ORS_API_BASE_URL + "geocode/search?api_key=" + ORS_API_KEY + "&text=" + fromAddress.Replace(" ", "%20"));
            RoutingWithBikeGeoCode.GeoCode departureGeoCode = JsonConvert.DeserializeObject<RoutingWithBikeGeoCode.GeoCode>(departureGeoCodeResponse);
            GeoCoordinate departureGeoCoordinate = new GeoCoordinate(departureGeoCode.features[0].geometry.coordinates[1], departureGeoCode.features[0].geometry.coordinates[0]);
            Station startStation = this.getClosestStation(departureGeoCoordinate, false);


            // TODO (possible extinction) : handle address erros : features list
            string arrivalGeoCodeResponse = doRequest(ORS_API_BASE_URL + "geocode/search?api_key=" + ORS_API_KEY + "&text=" + toAddress.Replace(" ", "%20"));
            RoutingWithBikeGeoCode.GeoCode arrivalGeoCode = JsonConvert.DeserializeObject<RoutingWithBikeGeoCode.GeoCode>(arrivalGeoCodeResponse);
            GeoCoordinate arrivalGeoCoordinate = new GeoCoordinate(arrivalGeoCode.features[0].geometry.coordinates[1], arrivalGeoCode.features[0].geometry.coordinates[0]);
            Station endStation = this.getClosestStation(arrivalGeoCoordinate, true);

            List<RoutingWithBikeItinirary.Itinirary> itiniraries = new List<RoutingWithBikeItinirary.Itinirary>();
            if (startStation == null)
            {
                System.Diagnostics.Debug.WriteLine("walking is better as no available start station was found");
                itiniraries.Add(getItinary(departureGeoCoordinate, arrivalGeoCoordinate));
                return itiniraries;
            }

            GeoCoordinate startStationGeoCoordinate = new GeoCoordinate(startStation.position.latitude, startStation.position.longitude);
            if (departureGeoCoordinate.GetDistanceTo(arrivalGeoCoordinate) <= departureGeoCoordinate.GetDistanceTo(startStationGeoCoordinate))
            {
                System.Diagnostics.Debug.WriteLine("walking is easier, as the closest station is far away from the destination");
                itiniraries.Add(getItinary(departureGeoCoordinate, arrivalGeoCoordinate));
                return itiniraries;
            }

            if (endStation == null)
            {
                System.Diagnostics.Debug.WriteLine("walking is better as no available end station was found");
                itiniraries.Add(getItinary(departureGeoCoordinate, startStationGeoCoordinate));
                itiniraries.Add(getItinary(startStationGeoCoordinate, arrivalGeoCoordinate));
                return itiniraries;
            }

            GeoCoordinate endStationGeoCoordinate = new GeoCoordinate(endStation.position.latitude, endStation.position.longitude);
            itiniraries.Add(getItinary(departureGeoCoordinate, startStationGeoCoordinate));
            itiniraries.Add(getItinary(startStationGeoCoordinate, endStationGeoCoordinate));
            itiniraries.Add(getItinary(endStationGeoCoordinate, arrivalGeoCoordinate));
            return itiniraries;
        }

    }

}
