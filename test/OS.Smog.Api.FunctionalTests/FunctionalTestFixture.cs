using OS.DAL.PgSql;
using OS.Docker.TestKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OS.Smog.Api.FunctionalTests
{
    public abstract class FunctionalTestFixture : DockerComposeFixture
    {
        public DeviceDbContext Context { get; }

        private const string DbName = "os-devices-integration-tests-db";
        private const string DbPassword = "postgres";
        private const ushort DbPort = 5432;
        private const string DbUser = "postgres";
        public override bool CleanUp => true;
        //public string ConnectionString => $"host=os-devices-db.svc;port={HostPort};database={DbName};username={DbUser};password={DbPassword}";

        protected FunctionalTestFixture()
        {
            //var builder = new DbContextOptionsBuilder<DeviceDbContext>()
            //    .UseNpgsql(ConnectionString, x => x.MigrationsAssembly(MigrationsAssembly.Assembly));

            //Context = new DeviceDbContext(builder.Options);
            //Context.Database.Migrate();
        }

        public override IReadOnlyList<string> ComposeFiles { get; } = new List<string>()
        {
            "docker-compose.yml",
            "docker-compose.functionalTests.yml"
        };

        protected override Task<bool> WaitForContainerInitialization(TimeSpan timeout)
        {
            return Task.FromResult(true);
        }
    }
}