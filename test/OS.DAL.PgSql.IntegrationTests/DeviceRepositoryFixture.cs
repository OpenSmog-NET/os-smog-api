using Microsoft.EntityFrameworkCore;
using OS.Core.Queries;
using OS.DAL.PgSql.Migrator;
using OS.Domain;

namespace OS.DAL.PgSql.IntegrationTests
{
    public class DeviceRepositoryFixture : PostgresFixture
    {
        public DeviceDbContext Context { get; }

        public long VendorId { get; }

        public DeviceRepositoryFixture()
        {
            var builder = new DbContextOptionsBuilder<DeviceDbContext>()
                .UseNpgsql(ConnectionString, x => x.MigrationsAssembly(MigrationsAssembly.Assembly));

            Context = new DeviceDbContext(builder.Options);
            Context.Database.Migrate();

            var repository = new VendorRepository(Context, new VendorMapper());
            VendorId = repository.Insert(new Vendor() { Name = "OpenSmog", Url = "http://opensmog.org" });
        }

        public Device[] EnsureDevicesAreInserted(string fileName, DeviceRepository repository, FilterCriterium criterium)
        {
            var devices = fileName.Get<Device>(d =>
            {
                d.VendorId = this.VendorId;
                return d;
            });
            var query = new Query();
            query.FilterCriteria.Add(criterium);

            if (repository.Count(query) != 0) return devices;

            repository.Insert(devices);

            return devices;
        }
    }
}