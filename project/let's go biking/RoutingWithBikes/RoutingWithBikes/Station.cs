using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RoutingWithBike
{
    public class Position
    {
        [DataMember]
        [JsonPropertyName("latitude")]
        public double latitude { get; set; }
        [DataMember]
        [JsonPropertyName("longitude")]
        public double longitude { get; set; }
    }

    public class Availabilities
    {
        [DataMember]
        [JsonPropertyName("bikes")]
        public int bikes { get; set; }
        [DataMember]
        [JsonPropertyName("stands")]
        public int stands { get; set; }
        [DataMember]
        [JsonPropertyName("mechanicalBikes")]
        public int mechanicalBikes { get; set; }
        [DataMember]
        [JsonPropertyName("electricalBikes")]
        public int electricalBikes { get; set; }
        [DataMember]
        [JsonPropertyName("electricalInternalBatteryBikes")]
        public int electricalInternalBatteryBikes { get; set; }
        [DataMember]
        [JsonPropertyName("electricalRemovableBatteryBikes")]
        public int electricalRemovableBatteryBikes { get; set; }
    }

    public class TotalStands
    {
        [DataMember]
        [JsonPropertyName("availabilities")]
        public Availabilities availabilities { get; set; }
        [DataMember]
        [JsonPropertyName("capacity")]
        public int capacity { get; set; }
    }

    public class MainStands
    {
        [DataMember]
        [JsonPropertyName("availabilities")]
        public Availabilities availabilities { get; set; }
        [DataMember]
        [JsonPropertyName("capacity")]
        public int capacity { get; set; }
    }

    public class Station
    {
        [DataMember]
        [JsonPropertyName("number")]
        public int number { get; set; }
        [DataMember]
        [JsonPropertyName("contractName")]
        public string contractName { get; set; }
        [DataMember]
        [JsonPropertyName("name")]
        public string name { get; set; }
        [DataMember]
        [JsonPropertyName("address")]
        public string address { get; set; }
        [DataMember]
        [JsonPropertyName("position")]
        public Position position { get; set; }
        [DataMember]
        [JsonPropertyName("banking")]
        public bool banking { get; set; }
        [DataMember]
        [JsonPropertyName("bonus")]
        public bool bonus { get; set; }
        [DataMember]
        [JsonPropertyName("status")]
        public string status { get; set; }
        [DataMember]
        [JsonPropertyName("lastUpdate")]
        public string lastUpdate { get; set; }
        [DataMember]
        [JsonPropertyName("connected")]
        public bool connected { get; set; }
        [DataMember]
        [JsonPropertyName("overflow")]
        public bool overflow { get; set; }
        [DataMember]
        [JsonPropertyName("shape")]
        public object shape { get; set; }
        [DataMember]
        [JsonPropertyName("totalStands")]
        public TotalStands totalStands { get; set; }
        [DataMember]
        [JsonPropertyName("mainStands")]
        public MainStands mainStands { get; set; }
        [DataMember]
        [JsonPropertyName("overflowStands")]
        public object overflowStands { get; set; }
    }

    public class GetResult
    {
        [DataMember]
        [JsonPropertyName("getResult")]
        public string getResult { get; set; }
    }

}