using System;

namespace OS.Domain
{
    public class Device : IAggregateRoot
    {
        public Guid Id { get; set; }
        public PostalAddress Address { get; set; }
        public Location Location { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
    }
}