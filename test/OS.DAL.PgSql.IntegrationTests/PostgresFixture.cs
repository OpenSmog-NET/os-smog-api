using Npgsql;
using OS.Docker.TestKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OS.DAL.PgSql.IntegrationTests
{
    public class PostgresFixture : DockerFixture
    {
        private const string DbName = "test";
        private const string DbPassword = "postgres";
        private const string DbUser = "postgres";
        private const ushort DbPort = 5432;
        public readonly ushort HostPort = PortManager.GetAvailablePort(5342);

        public PostgresFixture()
        {
        }

        public string ConnectionString => $"host=localhost;port={HostPort};database={DbName};username={DbUser};password={DbPassword}";
        public override string ContainerImageName { get; } = "postgres:alpine";
        public override string ContainerName { get; } = "os-dal-pgsql-integration-tests";

        public override IReadOnlyDictionary<string, string> EnvironmentVariables => new Dictionary<string, string>()
        {
            { "POSTGRES_USER", DbUser },
            { "POSTGRES_PASSWORD", DbPassword },
            { "POSTGRES_DB", DbName }
        };

        public override IReadOnlyDictionary<ushort, ushort> PortMappings => new Dictionary<ushort, ushort>
        {
            { HostPort, DbPort }
        };

        public override IReadOnlyDictionary<string, string> VolumeMappings { get; }

        protected override async Task<bool> WaitForContainerInitialization(TimeSpan timeout)
        {
            var startTime = DateTime.Now;
            while (DateTime.Now - startTime < timeout)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(ConnectionString))
                    {
                        conn.Open();
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            return true;
                        }
                    }
                }
                catch
                {
                    // (!) Intentionally ignore exceptions and keep retrying until timedout
                }

                await Task.Delay(1000).ConfigureAwait(false);
            }

            return false;
        }
    }
}