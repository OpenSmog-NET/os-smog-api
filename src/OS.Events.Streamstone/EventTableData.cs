using System;

namespace OS.Events.Streamstone
{
    internal class EventTableData
    {
        public Guid EventType { get; set; }
        public byte[] Data { get; set; }

        public int Version { get; set; }
    }
}