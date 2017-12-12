using Newtonsoft.Json;

namespace OS.Domain
{
    /// <summary>
    /// Device Geographical Location
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Geographical Longitude
        /// </summary>
        [JsonProperty("long")]
        public double Lon { get; set; }

        /// <summary>
        /// Geographical Latitude
        /// </summary>
        [JsonProperty("lat")]
        public double Lat { get; set; }
    }
}