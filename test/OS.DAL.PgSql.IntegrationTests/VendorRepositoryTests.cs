using Microsoft.EntityFrameworkCore;
using OS.DAL.PgSql.Migrator;
using Xunit;

namespace OS.DAL.PgSql.IntegrationTests
{
    public class VendorRepositoryTests : IClassFixture<PostgresFixture>
    {
        private readonly PostgresFixture fixture;
        private readonly DeviceDbContext context;

        public VendorRepositoryTests(PostgresFixture fixture)
        {
            this.fixture = fixture;

            var builder = new DbContextOptionsBuilder<DeviceDbContext>()
                .UseNpgsql(fixture.ConnectionString, x => x.MigrationsAssembly(MigrationsAssembly.Assembly));

            context = new DeviceDbContext(builder.Options);
            context.Database.Migrate();
        }

        [Fact]
        public void Test1()
        {
            context.Database.EnsureCreated();
        }
    }
}