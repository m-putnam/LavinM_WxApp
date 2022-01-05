using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WxZone
{
    public class Rootobject
    {
        public Context context { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Context
    {
        public string version { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        [JsonIgnore]
        public float[][][] coordinates { get; set; }
    }

    public class Properties
    {
        public string atId { get; set; }
        public string atType { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public DateTime effectiveDate { get; set; }
        public DateTime expirationDate { get; set; }
        public string state { get; set; }
        public string[] cwa { get; set; }
        public string[] forecastOffices { get; set; }
        public string[] timeZone { get; set; }
        [JsonIgnore]
        public object observationStations { get; set; }
        public string radarStation { get; set; }
    }
}
