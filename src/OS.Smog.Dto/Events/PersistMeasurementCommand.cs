using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Dto.Events
{
    [DataContract]
    public class PersistMeasurementCommand : IntegrationEvent
    {
        public PersistMeasurementCommand(Guid correlationId, Guid deviceId, Measurement measurement)
            : this(correlationId)
        {
            if (correlationId == null) throw new ArgumentNullException(nameof(correlationId));
            if (deviceId == null) throw new ArgumentNullException(nameof(deviceId));

            DeviceId = deviceId;
            Measurement = measurement;
        }
        private PersistMeasurementCommand(Guid correlationId) 
            : base(correlationId)
        {
        }

        [DataMember(Order = 2)]
        public Guid DeviceId { get; }

        [DataMember(Order = 3)]
        public Measurement Measurement { get; }
    }
}
