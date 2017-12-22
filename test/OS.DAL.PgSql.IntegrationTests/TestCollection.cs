using Xunit;

namespace OS.DAL.PgSql.IntegrationTests
{
    [CollectionDefinition(Constants.VendorRepositoryTestsCollection)]
    public class TestCollection : ICollectionFixture<VendorRepositoryFixture>
    {
    }

    [CollectionDefinition(Constants.DeviceRepositoryTestsCollection)]
    public class DeviceRepositoryTestsCollection : ICollectionFixture<DeviceRepositoryFixture>
    { }
}