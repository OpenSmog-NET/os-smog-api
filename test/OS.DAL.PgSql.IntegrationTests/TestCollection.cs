using Xunit;

namespace OS.DAL.PgSql.IntegrationTests
{
    public static class Constants
    {
        public const string IntegrationTestsCollection = "IntegrationTests";
    }

    [CollectionDefinition(Constants.IntegrationTestsCollection)]
    public class TestCollection : ICollectionFixture<PostgresFixture>
    {
    }
}