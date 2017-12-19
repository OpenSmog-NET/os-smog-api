using Xunit;

namespace OS.DAL.PgSql.IntegrationTests
{
    [CollectionDefinition(Constants.IntegrationTestsCollection)]
    public class TestCollection : ICollectionFixture<PostgresFixture>
    {
    }
}