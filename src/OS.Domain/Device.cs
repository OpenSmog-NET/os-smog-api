using Newtonsoft.Json;
using System;

namespace OS.Domain
{
    public class Device
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("address")]
        public PostalAddress Address { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vendorId")]
        public long VendorId { get; set; }

        [JsonProperty("type")]
        public DeviceType Type { get; set; }
    }
}