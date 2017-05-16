using System.Collections.Generic;
using OS.Events;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Events.Sensor
{
    public static class SensorAggregate
    {
        public static IEnumerable<IEvent> Register(State state, Measurement measurement)
        {
            if (state.TimeStamp > measurement.Timestamp)
                yield break;

            yield return new MeasurementRegistered(measurement);
        }

        public class State : BaseState
        {
            public int TimeStamp { get; set; }

            /// <summary>
            ///     Particulate Matter PM2.5 [ug/m3]
            /// </summary>
            public double? Pm25 { get; set; }

            /// <summary>
            ///     Particulate Matter PM10 [ug/m3]
            /// </summary>
            public double? Pm10 { get; set; }

            /// <summary>
            ///     Temperature [C]
            /// </summary>
            public double? Temp { get; set; }

            /// <summary>
            ///     Nitrogen Dioxide [ug/m3]
            /// </summary>
            public double? NO2 { get; set; }

            /// <summary>
            ///     Ozone [ug/m3]
            /// </summary>
            public double? O3 { get; set; }

            /// <summary>
            ///     Sulfur Dioxide [ug/m3]
            /// </summary>
            public double? SO2 { get; set; }

            /// <summary>
            ///     Air Humidity [%]
            /// </summary>
            public double? Hum { get; set; }

            /// <summary>
            ///     Lead [ug/m3]
            /// </summary>
            public double? Pb { get; set; }

            /// <summary>
            ///     Carbon Monoxide [ug/m3]
            /// </summary>
            public double? CO { get; set; }

            /// <summary>
            ///     Pressure [hPa]
            /// </summary>
            public double? Press { get; set; }

            public void Apply(MeasurementRegistered @event)
            {
                TimeStamp = @event.Timestamp;
                Pm10 = @event.Pm10;
                Pm25 = @event.Pm25;
                CO = @event.CO;
                Hum = @event.Hum;
                NO2 = @event.NO2;
                O3 = @event.O3;
                Pb = @event.Pb;
                Press = @event.Press;
                SO2 = @event.SO2;
                Temp = @event.Temp;
            }
        }
    }
}