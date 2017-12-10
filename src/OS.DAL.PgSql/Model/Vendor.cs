using System.Collections.Generic;

namespace OS.DAL.PgSql.Model
{
    public class Vendor : Entity<long>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public List<VendorApiKey> Keys { get; set; } = new List<VendorApiKey>();

        public List<Device> Devices { get; set; } = new List<Device>();
    }
}