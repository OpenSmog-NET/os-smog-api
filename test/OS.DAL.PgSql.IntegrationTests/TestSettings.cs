using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace OS.DAL.PgSql.IntegrationTests
{
    public static class Constants
    {
        public const string IntegrationTestsCollection = "IntegrationTests";
    }
}