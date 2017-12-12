namespace OS.DAL.PgSql.Model
{
    public class VendorApiKey : Entity<long>
    {
        public string Key { get; set; }
        public int Limit { get; set; }

        public long VendorId { get; set; }
    }
}