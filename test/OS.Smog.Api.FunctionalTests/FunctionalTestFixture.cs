using Microsoft.EntityFrameworkCore;
using OS.DAL.PgSql;
using OS.DAL.PgSql.Migrator;
using OS.Docker.TestKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OS.Smog.Api.FunctionalTests
{
    [Trait("FunctionalTests", "DeviceRepositoryTests")]
    public sealed class FunctionalTestFixture : DockerComposeFixture
    {
        private const string DbNameEnvVariable = "POSTGRES_DB";
        private const string DbUsernameEnvVariable = "POSTGRES_USER";
        private const string DbPasswordEnvVariable = "POSTGRES_PASSWORD";
        private const int DbHostPort = 5432;
        public DeviceDbContext Context { get; }

        public override bool CleanUp => true;
        public string ExternalDevicesDbConnectionString { get; }

        public FunctionalTestFixture()
        {
            var settings = DockerComposeFixture.ParseDockerComposeSettings(GetFullPath(ComposeFiles[1]));
            var devicesDbService = settings.Services["os-devices-db.svc"];

            ExternalDevicesDbConnectionString = $"host=localhost;port={devicesDbService.Ports[DbHostPort]};database={devicesDbService.Environment[DbNameEnvVariable]};username={devicesDbService.Environment[DbUsernameEnvVariable]};password={devicesDbService.Environment[DbPasswordEnvVariable]}"; ;

            var builder = new DbContextOptionsBuilder<DeviceDbContext>()
                .UseNpgsql(ExternalDevicesDbConnectionString, x => x.MigrationsAssembly(MigrationsAssembly.Assembly));

            Context = new DeviceDbContext(builder.Options);
            Context.Database.Migrate();
        }

        public override IReadOnlyList<string> ComposeFiles { get; } = new List<string>()
        {
            "docker-compose.yml",
            "docker-compose.functional-tests.yml"
        };

        protected override async Task<bool> ValidateComposeStartupAsync(TimeSpan timeout)
        {
            var validators = new IContainerStartupValidator[]
            {
                new PostgresStartupValidator()
            };

            var tasks = new[]
            {
                validators[0].ValidateContainerStartupAsync(timeout, ExternalDevicesDbConnectionString)
            };

            Task.WaitAll(tasks);

            return tasks.All(t => t.IsCompletedSuccessfully) && tasks.All(t => t.Result);
        }
    }
}