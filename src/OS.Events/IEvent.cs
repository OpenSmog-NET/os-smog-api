using System;

namespace OS.Events
{
    public interface IEvent
    {
        Guid EventId { get; }
    }
}