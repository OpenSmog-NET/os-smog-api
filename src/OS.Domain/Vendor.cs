using System.Collections.Generic;

namespace OS.Domain
{
    /// <summary>
    /// Device Vendor
    /// </summary>
    public class Vendor : IAggregateRoot
    {
        public List<Device> Devices { get; set; } = new List<Device>();
        public long Id { get; set; }

        public List<VendorApiKey> Keys { get; set; } = new List<VendorApiKey>();
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }
    }
}