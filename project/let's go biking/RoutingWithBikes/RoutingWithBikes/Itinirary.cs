using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RoutingWithBikeItinirary
{
    [DataContract]
    public class Step
    {
        [DataMember]
        [JsonPropertyName("distance")]
        public double distance { get; set; }
        [DataMember]
        [JsonPropertyName("duration")]
        public double duration { get; set; }
        [DataMember]
        [JsonPropertyName("type")]
        public int type { get; set; }
        [DataMember]
        [JsonPropertyName("instruction")]
        public string instruction { get; set; }
        [DataMember]
        [JsonPropertyName("name")]
        public string name { get; set; }
        [DataMember]
        [JsonPropertyName("way_points")]
        public List<int> way_points { get; set; }
    }

    [DataContract]
    public class Segment
    {
        [DataMember]
        [JsonPropertyName("distance")]
        public double distance { get; set; }
        [DataMember]
        [JsonPropertyName("duration")]
        public double duration { get; set; }
        [DataMember]
        [JsonPropertyName("steps")]
        public List<Step> steps { get; set; }
    }

    [DataContract]
    public class Summary
    {
        [DataMember]
        [JsonPropertyName("distance")]
        public double distance { get; set; }
        [DataMember]
        [JsonPropertyName("duration")]
        public double duration { get; set; }
    }

    [DataContract]
    public class Properties
    {
        [DataMember]
        [JsonPropertyName("segments")]
        public List<Segment> segments { get; set; }
        [DataMember]
        [JsonPropertyName("summary")]
        public Summary summary { get; set; }
        [DataMember]
        [JsonPropertyName("way_points")]
        public List<int> way_points { get; set; }
    }

    [DataContract]
    public class Geometry
    {
        [DataMember]
        [JsonPropertyName("coordinates")]
        public List<List<double>> coordinates { get; set; }
        [DataMember]
        [JsonPropertyName("type")]
        public string type { get; set; }
    }

    [DataContract]
    public class Feature
    {
        [DataMember]
        [JsonPropertyName("bbox")]
        public List<double> bbox { get; set; }
        [DataMember]
        [JsonPropertyName("type")]
        public string type { get; set; }
        [DataMember]
        [JsonPropertyName("properties")]
        public Properties properties { get; set; }
        [DataMember]
        [JsonPropertyName("geometry")]
        public Geometry geometry { get; set; }
    }

    [DataContract]
    public class Query
    {
        [DataMember]
        [JsonPropertyName("coordinates")]
        public List<List<double>> coordinates { get; set; }
        [DataMember]
        [JsonPropertyName("profile")]
        public string profile { get; set; }
        [DataMember]
        [JsonPropertyName("format")]
        public string format { get; set; }
    }

    [DataContract]
    public class Engine
    {
        [DataMember]
        [JsonPropertyName("version")]
        public string version { get; set; }
        [DataMember]
        [JsonPropertyName("build_date")]
        public DateTime build_date { get; set; }
        [DataMember]
        [JsonPropertyName("graph_date")]
        public DateTime graph_date { get; set; }
    }

    [DataContract]
    public class Metadata
    {
        [DataMember]
        [JsonPropertyName("attribution")]
        public string attribution { get; set; }
        [DataMember]
        [JsonPropertyName("service")]
        public string service { get; set; }
        [DataMember]
        [JsonPropertyName("timestamp")]
        public long timestamp { get; set; }
        [DataMember]
        [JsonPropertyName("query")]
        public Query query { get; set; }
        [DataMember]
        [JsonPropertyName("engine")]
        public Engine engine { get; set; }
    }

    [DataContract]
    public class Itinirary
    {
        [DataMember]
        [JsonPropertyName("type")]
        public string type { get; set; }

        [DataMember]
        [JsonPropertyName("features")]
        public List<Feature> features { get; set; }

        [DataMember]
        [JsonPropertyName("bbox")]
        public List<double> bbox { get; set; }

        [DataMember]
        [JsonPropertyName("metadata")]
        public Metadata metadata { get; set; }
    }

}
