using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using OS.Core.WebJobs;
using OS.Events.Streamstone;
using OS.Smog.Domain.Sensors;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Job
{
    public class SmogWebJob
    {
        private const int CancellationInterval = 10;
        private readonly ILogger<SmogWebJob> logger;
        private readonly CloudTableClient tableClient;

        public SmogWebJob(ILogger<SmogWebJob> logger, WebJobSettings settings)
        {
            this.logger = logger;
            this.tableClient = CloudStorageAccount.Parse(settings.GetStorageConnectionString()).CreateCloudTableClient();
        }

        public async Task ProcessMeasurement([EventHubTrigger("os-smog-api-measurements")] Measurement measurement)
        {
            var table = tableClient.GetTableReference("measurements");
            await table.CreateIfNotExistsAsync();

            await table.GetPartition("")
                .GetStream()
                .Invoke<SensorAggregate.State>(s => SensorAggregate.Register(s, measurement));
        }

        [NoAutomaticTrigger]
        public async Task RunAsync()
        {
            logger.LogInformation("SmogWebJob is starting");

            var token = new WebJobsShutdownWatcher().Token;

            while (!token.IsCancellationRequested)
                await Task.Delay(TimeSpan.FromSeconds(CancellationInterval), token);

            logger.LogInformation("SmogWebJob is shutting down");
        }
    }
}