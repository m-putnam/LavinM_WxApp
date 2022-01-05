using System;
using System.Text.Json.Serialization;

namespace WxZoneAlert
{
    public class Rootobject
    {
        public object[] context { get; set; }
        public string type { get; set; }
        public Feature[] features { get; set; }
        public string title { get; set; }
        public DateTime updated { get; set; }
    }

    public class Feature
    {
        public string id { get; set; }
        public string type { get; set; }
        public object geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Properties
    {
        public string atId { get; set; }
        public string atType { get; set; }
        public string id { get; set; }
        public string areaDesc { get; set; }
        public Geocode geocode { get; set; }
        public string[] affectedZones { get; set; }
        public Reference[] references { get; set; }
        public DateTime sent { get; set; }
        public DateTime effective { get; set; }
        public DateTime onset { get; set; }
        public DateTime expires { get; set; }
        public DateTime ends { get; set; }
        public AlertStatus status { get; set; }
        public AlertMessageType messageType { get; set; }
        public AlertCategory category { get; set; }
        public AlertSeverity Severity { get; set; }
        public AlertCertainty Certainty { get; set; }
        public AlertUrgency Urgency { get; set; }
        public string _event { get; set; }
        public string sender { get; set; }
        public string senderName { get; set; }
        public string headline { get; set; }
        public string description { get; set; }
        public string instruction { get; set; }
        public AlertResponse response { get; set; }
        public Parameters parameters { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AlertStatus
    {
        Actual, Exercise, System, Test, Draft
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AlertMessageType
    {
        Alert, Update, Cancel, Ack, Error
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AlertCategory
    {
        Met, Geo, Safety, Security, Rescue, Fire, Health, Env, Transport,
        Infra, CBRNE, Other
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AlertSeverity
    {
        Extreme, Severe, Moderate, Minor, Unknown
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AlertCertainty
    {
        Observed, Likely, Possible, Unlikely, Unknown
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AlertUrgency
    {
        Immediate, Expected, Future, Past, Unknown
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AlertResponse
    {
        Shelter, Evacuate, Prepare, Execute, Avoid, Monitor, Assess,
        AllClear, None
    }

    public class Geocode
    {
        public string[] SAME { get; set; }
        public string[] UGC { get; set; }
    }

    public class Parameters
    {
        public string[] PIL { get; set; }
        public string[] NWSheadline { get; set; }
        public string[] BLOCKCHANNEL { get; set; }
        public string[] VTEC { get; set; }
        public DateTime[] eventEndingTime { get; set; }
        public string[] EASORG { get; set; }
        public string[] expiredReferences { get; set; }
    }

    public class Reference
    {
        public string id { get; set; }
        public string identifier { get; set; }
        public string sender { get; set; }
        public DateTime sent { get; set; }
    }
}
