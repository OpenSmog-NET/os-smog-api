using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.Sensors
{
    public class MeasurementRegistered : IEvent
    {
        public Measurement Measurement { get; }

        public MeasurementRegistered(Measurement measurement)
        {
            Measurement = measurement;
        }
    }
}
