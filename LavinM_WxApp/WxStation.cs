using System;

namespace Station
{
    public class Rootobject 
    {
        public object[] context { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public decimal[] coordinates { get; set; }
    }

    public class Properties
    {
        public string id { get; set; }
        public string type { get; set; }
        public Elevation elevation { get; set; }
        public string stationIdentifier { get; set; }
        public string name { get; set; }
        public string timeZone { get; set; }
        public string forecast { get; set; }
        public string county { get; set; }
        public string fireWeatherZone { get; set; }
    }

    public class Elevation
    {
        public string unitCode { get; set; }
        public Nullable<decimal> value { get; set; }
    }
}
