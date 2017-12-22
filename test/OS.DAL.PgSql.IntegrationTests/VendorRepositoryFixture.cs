using Microsoft.EntityFrameworkCore;
using OS.DAL.PgSql.Migrator;

namespace OS.DAL.PgSql.IntegrationTests
{
    public class VendorRepositoryFixture : PostgresFixture
    {
        public DeviceDbContext Context { get; }

        public VendorRepositoryFixture()
        {
            var builder = new DbContextOptionsBuilder<DeviceDbContext>()
                .UseNpgsql(ConnectionString, x => x.MigrationsAssembly(MigrationsAssembly.Assembly));

            Context = new DeviceDbContext(builder.Options);
            Context.Database.Migrate();
        }
    }
}