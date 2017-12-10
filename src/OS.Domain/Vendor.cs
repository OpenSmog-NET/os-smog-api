namespace OS.Domain
{
    /// <summary>
    /// Device Vendor
    /// </summary>
    public class Vendor : IAggregateRoot
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}