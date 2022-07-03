using System;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.Geolocation;

/* A weather app, featuring plenty of async, await, and Task<T>
for plenty of fun.  Utilizes WinRT geolocation API and National
Weather Service web API in combination to display both current
conditions and a one-week forecast. */
namespace LavinM_WxApp
{
    public partial class WxForm : Form
    {
        /* Almost all requests work off of this base URL, excepting the few
        tasks which use an absolute URL which still remains within the API. */
        private const string wxUrl = "https://api.weather.gov/";
        /* Requests tend to get eaten by 403s if I pick a custom user agent the
        server doesn't like. */
        private const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64;"
            + "x64; rv:93.0) Gecko/20100101 Firefox/93.0";
        private static readonly HttpClient client = new();
        /* Cardinal directions */
        private static readonly string[] dirs = {"N", "NNE", "NE", "ENE", "E", "ESE",
            "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW"};
        /* Our actual wind-chill formula */
        static readonly Func<double, double, double> fChill = (tAir, vWind) =>
            13.12 + 0.6215 * tAir - 11.37 * Math.Pow(vWind, 0.16)
                + 0.3965 * tAir * Math.Pow(vWind, 0.16);

        /* Take the properties field of a WxPoint and return the
        location of its corresponding zone forecast */
        private static readonly Func<WxPoint.Properties, string> pointToGridLoc =
            p => $"gridpoints/{p.gridId}/{p.gridX},{p.gridY}/forecast";

        /* Take the string ID of a forecast zone and return the location of its
        corresponding alerts, if any. */
        private static readonly Func<string, string> zoneToAlertLoc =
            id => $"alerts/active/zone/{id}";

        public WxForm()
        {
            InitializeComponent();
            /* All of our requests are to the same base URI */
            client.BaseAddress = new Uri(wxUrl);
            client.DefaultRequestHeaders.UserAgent.TryParseAdd(userAgent);
            /* We don't strictly have GeoJSON support, but we want a specific
            format in return which matches our schema. */
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/geo+json"));
        }

        /* Get latest observation of given station from web API. */
        private async Task<Observation.Rootobject> GetObservation(string station)
        {
            Observation.Rootobject o = null;
            try
            {
                o = await client.GetFromJsonAsync<Observation.Rootobject>($"stations/{station}/observations/latest");
            }
            catch (Exception e)
            {
                BoxedError(e.Message);
            }
            return o;
        }

        /* Get info on WX station from API */
        private async Task<Station.Rootobject> GetStation(string station)
        {
            Station.Rootobject s = null;
            try
            {
                s = await client.GetFromJsonAsync<Station.Rootobject>($"stations/{station}");
            }
            catch (Exception err)
            {
                BoxedError(err.Message);
            }
            return s;
        }

        /* Get our specified station from the text box, otherwise
        defaulting to Keene. */
        private async void ButtonRetrieve_Click(object sender, EventArgs e)
        {
            string station;
            try
            {
                string pattern = @"^\s*(?<station>\p{L}{4})\s*$";
                Match match = Regex.Match(textRequest.Text, pattern);
                Group grp = match.Groups["station"];
                station = grp.Success ? grp.Value.ToUpper() : "KEEN";
                Station.Rootobject stObj = await GetStation(station);
                WxPoint.Rootobject StationPt =
                    await GetPoint((double) stObj.geometry.coordinates[1],
                    (double) stObj.geometry.coordinates[0]);
                this.UseWaitCursor = true;
                GetForecast(StationPt);
            }
            catch (Exception err)
            {
                BoxedError(err.Message);
            }
        }

        /* Give it a string, and it'll show a standard error dialog */
        private static void BoxedError(string s)
        {
            MessageBox.Show(s,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void BoxedError(Exception err)
        {
            BoxedError(err.Message);
        }

        /* Same with an alert, just to make development straightforward. */
        private static void BoxedAlert(String s)
        {
            MessageBox.Show(s,
                "Message",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }

        /*
        Take our weather point, acquired from geolocation or otherwise, and
        return an array of forecast Periods. Each Period contains the various
        data points needed to populate our forecast field.
        */
        private async void GetForecast(WxPoint.Rootobject rootPoint)
        {
            /* I don't feel like getting into the unit testing capabilities of
            C# just to bring in the assert class. */
            if (rootPoint == null)
            {
                BoxedError("Unable to retrieve forecast at location.");
                return;
            }
            string forecastLoc = pointToGridLoc(rootPoint.properties);
            GetZoneForPoint(rootPoint);
            WxForecast.Period[] forecastPeriods = Array.Empty<WxForecast.Period>();
            try
            {
                WxForecast.Rootobject rootForecast =
                    await client.GetFromJsonAsync<WxForecast.Rootobject>(forecastLoc);
                forecastPeriods = rootForecast.properties.Periods;
            }
            catch (Exception err)
            {
                BoxedError(err);
            }
            /* Clear the UI before adding the forecast, then add each period in
            the seven-day forecast. */
            tableForecast.Controls.Clear();
            foreach (WxForecast.Period p in forecastPeriods)
            {
                GroupBox pBox = new();
                /* Bold label for name of each forecast period */
                pBox.Font = new(pBox.Font, pBox.Font.Style | FontStyle.Bold);
                pBox.Text = p.name;
                /* We want our box to grow to accomodate the height of long
                forecasts.  This took a good bit of finagling to work out. */
                pBox.AutoSize = true;
                pBox.AutoSizeMode = AutoSizeMode.GrowOnly;
                pBox.Dock = DockStyle.Fill;
                Label lblForecast = new();
                lblForecast.Text = p.detailedForecast;
                /* Labels inherit font style from the groupbox by default */
                lblForecast.Font = new(lblForecast.Font,
                    lblForecast.Font.Style & FontStyle.Regular);
                /* We allow automatic resizing, but only within the width of
                the containing groupbox.  Vertical is fair game. */
                lblForecast.AutoSize = true;
                lblForecast.MaximumSize = new(tableForecast.Width
                    - tableForecast.Margin.Horizontal
                    - pBox.Margin.Horizontal
                    - SystemInformation.VerticalScrollBarWidth, 0);
                /* Fill the containing element, just to make sure we're at the
                proper width. */
                lblForecast.Dock = DockStyle.Fill;
                /* Make sure the label doesn't expand the box past its limit */
                pBox.MaximumSize = new(tableForecast.Width
                    - tableForecast.Margin.Horizontal
                    - SystemInformation.VerticalScrollBarWidth, 0);
                /* Resize the box to accomodate the full height of the label */
                pBox.Size = new(tableForecast.Width
                    - tableForecast.Margin.Horizontal
                    - SystemInformation.VerticalScrollBarWidth,
                    lblForecast.Height + pBox.Margin.Vertical);
                /* Our label goes into the groupbox */
                pBox.Controls.Add(lblForecast);
                /* Finally, the groupbox is added to the table as a new row */
                tableForecast.Controls.Add(pBox);
            }
            this.UseWaitCursor = false;
        }

        /* Get the zone which contains the given point */
        private static async void GetZoneForPoint(WxPoint.Rootobject rtPt)
        {
            string zoneUrl = rtPt.properties.forecastZone;
            WxZone.Rootobject Zone = null;
            try
            {
                Zone = await client.GetFromJsonAsync<WxZone.Rootobject>(zoneUrl);
            }
            catch (Exception err)
            {
                BoxedError(err);
            }
            if (Zone is WxZone.Rootobject zoneVal)
            {
                string zoneId = zoneVal.properties.id;
                GetZoneAlerts(zoneId);
            }
        }

        /* Get zone alerts for the given zone ID */
        private static async void GetZoneAlerts(string zoneId)
        {
            string alertLoc = zoneToAlertLoc(zoneId);
            WxZoneAlert.Rootobject rtAlert = null;
            try
            {
                rtAlert = await client.GetFromJsonAsync<WxZoneAlert.Rootobject>(alertLoc);
            }
            catch (Exception err)
            {
                BoxedError(err);
            }
            /* The list of alerts may be empty if none apply */
            if (rtAlert.features.Length > 0)
            {
                foreach (WxZoneAlert.Feature alert in rtAlert.features)
                {
                    BoxedAlert(alert.properties.headline);
                }
            }
        }

        /* Given latitude and longitude, retrieve and return the
        corresponding point object from the NWS web API.  Some claims in
        the documentation as of 5/10/2022 that gridpoints have an
        unintended offset of 1,1; this has not been observed in testing.
        */
        private async static Task<WxPoint.Rootobject> GetPoint(double lat, double lon)
        {
            string pointIdent = $"points/{lat:0.0000},{lon:0.0000}";
            using (var rtResponse = await client.GetAsync(pointIdent))
            {
                /* Temporary workaround for issue reported 5/10/2022, repeat request if endpoint returns 500. */
                if ((int) rtResponse.StatusCode == 500)
                    await client.GetAsync(pointIdent);
                //rtPt = await client.GetFromJsonAsync<WxPoint.Rootobject>(pointIdent);
                using (var ptStream = rtResponse.Content.ReadAsStream())
                {
                    return await JsonSerializer.DeserializeAsync<WxPoint.Rootobject>(ptStream);
                }
            }
        }

        /* Start off our geolocation logic using WinRT API, then send to
        forecast retrieval functions.  Now handles access status issues in a
        primitive way.  Potential access status is either specifically allowed,
        specifically denied, or unspecified. */
        private async void ButtonGeolocate_ClickAsync(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
            case GeolocationAccessStatus.Allowed:
                var Locator = new Geolocator();
                var Location = await Locator.GetGeopositionAsync();
                /* The full WinRT matryoshka experience. */
                var Pos = Location.Coordinate.Point.Position;
                var WxPoint = await GetPoint(Pos.Latitude, Pos.Longitude);
                GetForecast(WxPoint);
                break;
            case GeolocationAccessStatus.Denied:
                /* Fall back to the user's default geoposition as needed,
                assuming it's set.  Otherwise fail without retrieving
                forecast or alerts. */
                var defPos = Geolocator.DefaultGeoposition;
                if (defPos is BasicGeoposition posValue)
                {
                    var DefPoint = await GetPoint(posValue.Latitude, posValue.Longitude);
                    GetForecast(DefPoint);
                }
                else
                {
                    BoxedError("Geolocation access denied.  Check Windows settings.");
                }
                break;
            case GeolocationAccessStatus.Unspecified:
                /* As with denial, we fail without retrieval. */
                BoxedError("Unspecified geolocation error.");
                break;
            }
        }
    }
}
