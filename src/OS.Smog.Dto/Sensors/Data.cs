using Newtonsoft.Json;

namespace OS.Smog.Dto.Sensors
{
    public class Data
    {
        /// <summary>
        ///     Particulate Matter PM2.5 [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("pm2_5")]
        public double? Pm25 { get; set; }

        /// <summary>
        ///     Particulate Matter PM10 [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("pm10")]
        public double? Pm10 { get; set; }

        /// <summary>
        ///     Temperature [C] >= -273.15f (0.0 [K])
        /// </summary>
        [JsonProperty("temp")]
        public double? Temp { get; set; }

        /// <summary>
        ///     Nitrogen Dioxide [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("no2")]
        public double? NO2 { get; set; }

        /// <summary>
        ///     Ozone [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("o3")]
        public double? O3 { get; set; }

        /// <summary>
        ///     Sulfur Dioxide [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("so2")]
        public double? SO2 { get; set; }

        /// <summary>
        ///     Air Humidity [%] 0.0f - 100.0f
        /// </summary>
        [JsonProperty("hum")]
        public double? Hum { get; set; }

        /// <summary>
        ///     Lead [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("pb")]
        public double? Pb { get; set; }

        /// <summary>
        ///     Carbon Monoxide [ug/m3] >= 0.0f
        /// </summary>
        [JsonProperty("co")]
        public double? CO { get; set; }

        /// <summary>
        ///     Pressure [hPa] >= 0.0f
        /// </summary>
        [JsonProperty("press")]
        public double? Press { get; set; }
    }
}