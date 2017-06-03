using System;
using System.Runtime.Serialization;

namespace OS.Events
{
    public abstract class Event : IEvent
    {
        protected Event()
        {
            EventId = Guid.NewGuid();
        }

        [DataMember(Order = 0)]
        public Guid EventId { get; }
    }
}