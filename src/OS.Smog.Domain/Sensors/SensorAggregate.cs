using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.Sensors
{
    public static class SensorAggregate
    {
        public static IEnumerable<IEvent> Register(State state, Measurement measurement)
        {
            if (state.TimeStamp > measurement.Timestamp)
            {
                yield break;
            }

            yield return new MeasurementRegistered(measurement);
        }

        public class State
        {
            public void Apply(MeasurementRegistered @event)
            {
                var data = @event.Measurement.Data;

                TimeStamp = @event.Measurement.Timestamp;
                Pm10 = data.Pm10.ToNullableDouble();
                Pm25 = data.Pm25.ToNullableDouble();
                CO = data.CO.ToNullableDouble();
                Hum = data.Hum.ToNullableDouble();
                NO2 = data.NO2.ToNullableDouble();
                O3 = data.O3.ToNullableDouble();
                Pb = data.Pb.ToNullableDouble();
                Press = data.Press.ToNullableDouble();
                SO2 = data.SO2.ToNullableDouble();
                Temp = data.Temp.ToNullableDouble();
            }

            public int TimeStamp { get; set; }

            /// <summary>
            /// Particulate Matter PM2.5 [ug/m3]
            /// </summary>

            public double? Pm25 { get; set; }

            /// <summary>
            /// Particulate Matter PM10 [ug/m3]
            /// </summary>

            public double? Pm10 { get; set; }

            /// <summary>
            /// Temperature [C]
            /// </summary>

            public double? Temp { get; set; }

            /// <summary>
            /// Nitrogen Dioxide [ug/m3]
            /// </summary>

            public double? NO2 { get; set; }

            /// <summary>
            /// Ozone [ug/m3]
            /// </summary>

            public double? O3 { get; set; }

            /// <summary>
            /// Sulfur Dioxide [ug/m3]
            /// </summary>

            public double? SO2 { get; set; }

            /// <summary>
            /// Air Humidity [%]
            /// </summary>

            public double? Hum { get; set; }

            /// <summary>
            /// Lead [ug/m3]
            /// </summary>

            public double? Pb { get; set; }

            /// <summary>
            /// Carbon Monoxide [ug/m3]
            /// </summary>

            public double? CO { get; set; }

            /// <summary>
            /// Pressure [hPa]
            /// </summary>

            public double? Press { get; set; }
        }
    }
}
