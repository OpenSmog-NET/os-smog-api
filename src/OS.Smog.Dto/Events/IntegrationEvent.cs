using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OS.Smog.Dto.Events
{
    /// <summary>
    /// An integration event is an event used to sync state between services.
    /// </summary>
    public abstract class IntegrationEvent
    {
        [JsonProperty("correlationId")]
        public Guid CorrelationId { get; }

        [JsonProperty("dispatchedAt")]
        public DateTime DispatchedAt { get; }

        protected IntegrationEvent(Guid correlationId)
        {
            DispatchedAt = DateTime.UtcNow;
            CorrelationId = correlationId;
        }
    }
}
