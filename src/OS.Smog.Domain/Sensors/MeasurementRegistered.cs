using System.Runtime.Serialization;
using OS.Events;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.Sensors
{
    [EventTypeId("5D8DAF27-51FB-4E7A-81EA-DCE8E9E9AC41")]
    [DataContract]
    public class MeasurementRegistered : Event
    {
        public MeasurementRegistered(Measurement measurement)
        {
            Timestamp = measurement.Timestamp;

            var data = measurement.Data;

            Pm10 = data.Pm10;
            Pm25 = data.Pm25;
            Temp = data.Temp;
            NO2 = data.NO2;
            O3 = data.O3;
            SO2 = data.SO2;
            Hum = data.Hum;
            Pb = data.Pb;
            CO = data.CO;
            Press = data.Press;
        }

        public MeasurementRegistered()
        {
        }

        [DataMember(Order = 1)]
        public int Timestamp { get; set; }

        [DataMember(Order = 2)]
        public double? Pm25 { get; set; }

        [DataMember(Order = 3)]
        public double? Pm10 { get; set; }

        [DataMember(Order = 4)]
        public double? Temp { get; set; }

        [DataMember(Order = 5)]
        public double? NO2 { get; set; }

        [DataMember(Order = 6)]
        public double? O3 { get; set; }

        [DataMember(Order = 7)]
        public double? SO2 { get; set; }

        [DataMember(Order = 8)]
        public double? Hum { get; set; }

        [DataMember(Order = 9)]
        public double? Pb { get; set; }

        [DataMember(Order = 10)]
        public double? CO { get; set; }

        [DataMember(Order = 11)]
        public double? Press { get; set; }
    }
}