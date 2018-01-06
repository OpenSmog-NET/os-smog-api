using Microsoft.EntityFrameworkCore;
using OS.DAL.PgSql.Migrator;
using OS.Docker.TestKit;

namespace OS.DAL.PgSql.IntegrationTests
{
    public class VendorRepositoryFixture : PostgresFixture
    {
        public VendorRepositoryFixture()
        {
            var builder = new DbContextOptionsBuilder<DeviceDbContext>()
                .UseNpgsql(ConnectionString, x => x.MigrationsAssembly(MigrationsAssembly.Assembly));

            Context = new DeviceDbContext(builder.Options);
            Context.Database.Migrate();
        }

        public override bool CleanUp => true;
        public override string ContainerName => "os-devices-db-integration-tests";
        public DeviceDbContext Context { get; }

        protected override PostgresConfig Config => new PostgresConfig("os-devices-integration-tests-db", "postgres", "postgres", 5432);
    }
}