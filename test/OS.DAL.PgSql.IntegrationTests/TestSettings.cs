using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace OS.DAL.PgSql.IntegrationTests
{
    public static class Constants
    {
        public const string VendorRepositoryTestsCollection = "VendorRepositoryTests";
        public const string DeviceRepositoryTestsCollection = "DeviceRepositoryTests";
    }
}