using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OS.Core;
using System.IO;

namespace OS.DAL.PgSql.Migrator
{
    public class DeviceDbContextProvider : IDesignTimeDbContextFactory<DeviceDbContext>
    {
        private IConfiguration Configuration { get; }

        public DeviceDbContextProvider()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .AddJsonFile($"appSettings.{HostingEnvironment.Name}.json", optional: true)
                .Build();
        }

        public DeviceDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DeviceDbContext>();
            builder.UseNpgsql(Configuration.GetConnectionString("Database"), x => x.MigrationsAssembly(MigrationsAssembly.Assembly));
            return new DeviceDbContext(builder.Options);
        }
    }
}