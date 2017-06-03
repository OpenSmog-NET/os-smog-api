using System;

namespace OS.Events
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EventTypeIdAttribute : Attribute
    {
        public readonly Guid Id;

        public EventTypeIdAttribute(string id)
        {
            Id = new Guid(id);
        }
    }
}