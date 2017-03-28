using Newtonsoft.Json;

namespace OS.Smog.Dto.Sensors
{
    public class Readings
    {
        /// <summary>
        /// Particulate Matter PM2.5 [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("pm2_5")]
        public float? Pm25 { get; set; }

        /// <summary>
        /// Particulate Matter PM10 [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("pm10")]
        public float? Pm10 { get; set; }

        /// <summary>
        /// Temperature [C] >= -273.15f (0.0 [K])
        /// </summary>
        [JsonProperty("temp")]
        public float? Temp { get; set; }

        /// <summary>
        /// Nitrogen Dioxide [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("no2")]
        public float? NO2 { get; set; }

        /// <summary>
        /// Ozone [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("o3")]
        public float? O3 { get; set; }

        /// <summary>
        /// Sulfur Dioxide [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("so2")]
        public float? SO2 { get; set; }

        /// <summary>
        /// Air Humidity [%] 0.0f - 100.0f
        /// </summary>
        [JsonProperty("hum")]
        public float? Hum { get; set; }

        /// <summary>
        /// Lead [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("pb")]
        public float? Pb { get; set; }

        /// <summary>
        /// Carbon Monoxide [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("co")]
        public float? CO { get; set; }

        /// <summary>
        /// Pressure [hPa] >= 0.0f
        /// </summary>
        [JsonProperty("press")]
        public float? Press { get; set; }
    }
}