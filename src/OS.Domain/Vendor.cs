using System.Collections.Generic;

namespace OS.Domain
{
    /// <summary>
    /// Device Vendor
    /// </summary>
    public class Vendor
    {
        public List<Device> Devices { get; set; } = new List<Device>();
        public long Id { get; set; }

        public List<VendorApiKey> Keys { get; set; } = new List<VendorApiKey>();
        public string Name { get; set; }

        public string Url { get; set; }
    }
}