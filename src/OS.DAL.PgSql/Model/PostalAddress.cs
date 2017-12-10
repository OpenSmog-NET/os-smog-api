﻿namespace OS.DAL.PgSql.Model
{
    public class PostalAddress : Entity<long>
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public Device Device { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}