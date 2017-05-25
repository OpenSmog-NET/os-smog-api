using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OS.Smog.Dto.Events
{
    /// <summary>
    /// An integration event is an event used to sync state between services.
    /// </summary>
    public abstract class IntegrationEvent
    {
        [DataMember(Order = 0)]
        public Guid CorrelationId { get; }

        [DataMember(Order = 1)]
        public DateTime DispatchedAt { get; }

        protected IntegrationEvent(Guid correlationId)
        {
            DispatchedAt = DateTime.UtcNow;
            CorrelationId = correlationId;
        }
    }
}
