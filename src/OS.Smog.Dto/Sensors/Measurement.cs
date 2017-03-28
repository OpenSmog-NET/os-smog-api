namespace OS.Smog.Dto.Sensors
{
    public class Measurement
    {
        /// <summary>
        /// Unix Epoch Time GMT+0000
        /// </summary>
        public int Timestamp { get; set; }

        public Readings Readings { get; set; } = new Readings();

        /// <summary>
        /// At least a single measurement has a value?
        /// </summary>
        public bool IsNotNull() => Readings.Temp.HasValue
                                 || Readings.CO.HasValue
                                 || Readings.Hum.HasValue
                                 || Readings.NO2.HasValue
                                 || Readings.Pb.HasValue
                                 || Readings.Pm10.HasValue
                                 || Readings.Pm25.HasValue
                                 || Readings.Press.HasValue
                                 || Readings.SO2.HasValue
                                 || Readings.O3.HasValue;
    }
}