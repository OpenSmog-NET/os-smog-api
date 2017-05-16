using Newtonsoft.Json;

namespace OS.Smog.Dto.Sensors
{
    public class Measurement
    {
        [JsonProperty("timestamp")]
        /// <summary>
        /// Unix Epoch Time GMT+0000
        /// </summary>
        public int Timestamp { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; } = new Data();

        /// <summary>
        ///     At least a single measurement has a value?
        /// </summary>
        public bool IsNotNull() => Data.Temp.HasValue
                                   || Data.CO.HasValue
                                   || Data.Hum.HasValue
                                   || Data.NO2.HasValue
                                   || Data.Pb.HasValue
                                   || Data.Pm10.HasValue
                                   || Data.Pm25.HasValue
                                   || Data.Press.HasValue
                                   || Data.SO2.HasValue
                                   || Data.O3.HasValue;
    }
}