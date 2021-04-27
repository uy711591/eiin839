function findItinirary() {
    var targetUrl = "http://localhost:8733/Design_Time_Addresses/RoutingWithBikes/RestService/closestStations";
    var requestType = "GET";
    var fromAddress = document.getElementById("fromAddress").value;
    var toAddress = document.getElementById("toAddress").value;
    var params = ["fromAddress=" + fromAddress, "toAddress=" + toAddress];

    var onFinish = displayItinerary;

    callAPI(targetUrl, requestType, params, onFinish);
}

function displayItinerary() {
    if (this.status !== 200) {
        console.log("Itinerary not retrieved. Check the error in the Network or Console tab.");
    } else {
        document.getElementById('item').innerHTML = "<div id='map'></div>";
        var map = L.map('map').setView([43.697370, 7.270170], 13);
        L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
            maxZoom: 18,
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, ' +
                'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            id: 'mapbox/light-v9',
            tileSize: 512,
            zoomOffset: -1
        }).addTo(map);
        var itinerary = JSON.parse(this.responseText);
        var markerNames = ['Depart', 'Station 1', 'Station 2', 'Arrival'];
        for (var i = 0; i < itinerary.getClosestStationsResult.length; i++) {
            L.geoJSON(itinerary.getClosestStationsResult[i].features).addTo(map);
            var long1 = itinerary.getClosestStationsResult[i].metadata.query.coordinates[0][0];
            var lat1 = itinerary.getClosestStationsResult[i].metadata.query.coordinates[0][1];
            var marker = L.marker([lat1, long1]).addTo(map);
            marker.bindPopup(markerNames[i]);
            if (i == 0) {
                map.panTo(new L.LatLng(lat1, long1));
            }
            if (i + 1 == itinerary.getClosestStationsResult.length ) {
                var long2 = itinerary.getClosestStationsResult[i].metadata.query.coordinates[1][0];
                var lat2 = itinerary.getClosestStationsResult[i].metadata.query.coordinates[1][1];
                var marker = L.marker([lat2, long2]).addTo(map);
                marker.bindPopup(markerNames[3]);
            }
        }
    }
}

function callAPI(url, requestType, params, finishHandler) {
    var fullUrl = url;

    // If there are params, we need to add them to the URL.
    if (params) {
        // Reminder: an URL looks like protocol://host?param1=value1&param2=value2 ...
        fullUrl += "?" + params.join("&");
    }

    // The js class used to call external servers is XMLHttpRequest.
    var caller = new XMLHttpRequest();
    caller.open(requestType, fullUrl, true);
    // The header set below limits the elements we are OK to retrieve from the server.
    caller.setRequestHeader ("Accept", "application/json");
    // onload shall contain the function that will be called when the call is finished.
    caller.onload=finishHandler;

    caller.send();
}