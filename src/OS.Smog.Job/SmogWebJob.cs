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
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using ProtoBuf;
using Serilog.Context;

namespace OS.Smog.Job
{
    public class SmogWebJob
    {
        public const string EventHubName = "os-smog-api-measurements";

        private const int CancellationInterval = 10;
        private readonly ILogger<SmogWebJob> logger;
        private readonly CloudTableClient tableClient;

        public SmogWebJob(ILogger<SmogWebJob> logger, WebJobSettings settings)
        {
            this.logger = logger;
            this.tableClient = CloudStorageAccount.Parse(settings.GetStorageConnectionString()).CreateCloudTableClient();
        }

        public async Task PersistMeasurement([EventHubTrigger(EventHubName)] string json)
        {
            var command = JsonConvert.DeserializeObject<PersistMeasurementCommand>(json);

            using (LogContext.PushProperty("CorrelationId", command.CorrelationId))
            {
                logger.LogInformation($"Received measurements from device {command.DeviceId}");

                var table = tableClient.GetTableReference("measurements");
                await table.CreateIfNotExistsAsync();

                var partitionId = $"{command.DeviceId.ToString()}-{DateTime.UtcNow.Date}";

                logger.LogDebug($"Persisting measurements to {@partitionId}");

                await table.GetPartition(partitionId)
                    .GetStream()
                    .Invoke<SensorAggregate.State>(s => SensorAggregate.Register(s, command.Measurement));
            }
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