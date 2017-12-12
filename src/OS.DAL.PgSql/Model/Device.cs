using System;

namespace OS.DAL.PgSql.Model
{
    public class Device : Entity<Guid>, IAggregateRoot
    {
        public PostalAddress Address { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public long VendorId { get; set; }
    }
}