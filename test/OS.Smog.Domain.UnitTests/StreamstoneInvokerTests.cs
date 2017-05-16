using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using OS.Events;
using OS.Events.Streamstone;
using OS.Smog.Domain.Sensors;
using OS.Smog.Dto.Sensors;
using Shouldly;
using Xunit;

namespace OS.Smog.Domain.UnitTests
{
    public class StreamstoneInvokerTests
    {
        [Fact]
        public async Task GivenEventsWereWrittenToStream_WhenReadingLatestState_ComputedStateShouldBeLatest()
        {
            // Arrange
            var client = CloudStorageAccount.DevelopmentStorageAccount.CreateCloudTableClient();
            var table = client.GetTableReference("test");

            await table.CreateIfNotExistsAsync()
                .ConfigureAwait(false);


            var events = new IEvent[]
            {
                new MeasurementRegistered(new Measurement {Timestamp = 1494848616, Data = new Data {Pm10 = 0.05}}),
                new MeasurementRegistered(new Measurement
                {
                    Timestamp = 1494848616,
                    Data = new Data {Pm10 = 0.06f, Pm25 = 10.0f}
                }),
                new MeasurementRegistered(new Measurement {Timestamp = 1494848617, Data = new Data {Pm10 = 0.07}}),
                new MeasurementRegistered(new Measurement {Timestamp = 1494848618, Data = new Data {Pm10 = 0.08}})
            };

            // Act
            var stream = table
                .GetPartition("test-partition-1")
                .GetStream();

            await stream
                .Invoke<SensorAggregate.State>(s => events)
                .ConfigureAwait(false);

            var state = await stream.ReadLatestAsync<SensorAggregate.State>()
                .ConfigureAwait(false);

            // Assert
            state.TimeStamp.ShouldBe(1494848618);
            state.Pm10.ShouldBe(0.08);
        }
    }
}