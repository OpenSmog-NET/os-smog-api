using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using OS.Core.WebJobs;
using OS.Events.Streamstone;
using OS.Smog.Dto.Events;
using OS.Smog.Events.Sensor;
using System;
using System.Threading.Tasks;

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

        public async Task PersistMeasurement(
            [EventHubTrigger("os-smog-api-measurements", Connection = "OS.Smog.Job", ConsumerGroup = "$Default")] PersistMeasurementCommand command)
        {
            // todo: storage account & table reference, can be retrieved based on device's registration data
            var table = tableClient.GetTableReference("measurements");
            await table.CreateIfNotExistsAsync();

            await table.GetPartition($"{command.DeviceId.ToString()}-{DateTime.UtcNow.Date}")
                .GetStream()
                .Invoke<SensorAggregate.State>(s => SensorAggregate.Register(s, command.Measurement));
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