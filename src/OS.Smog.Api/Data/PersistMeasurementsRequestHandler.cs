using Marten;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OS.Core.Queues;
using OS.Data.v1;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Smog.Api.Data
{
    public class PersistMeasurementsRequestHandler : IAsyncRequestHandler<PersistMeasurementsRequest, PersistMeasurementsResponse>
    {
        private readonly ILogger<PersistMeasurementsRequestHandler> logger;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IDocumentStore store;
        private readonly IQueueClient client;

        public PersistMeasurementsRequestHandler(
                ILogger<PersistMeasurementsRequestHandler> logger,
                IHttpContextAccessor contextAccessor,
                IDocumentStore store,
                IQueueClient client)
        {
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.store = store;
            this.client = client;
        }

        public async Task<PersistMeasurementsResponse> Handle(PersistMeasurementsRequest request)
        {
            //message = Guid.Parse(contextAccessor.HttpContext.Request.Headers[Constants.RequestCorrelation.RequestHeaderName]);

            using (var session = store.OpenSession())
            {
                var latestMeasurement = await session.Query<MeasurementData>()
                    .Where(x => x.DeviceId == request.DeviceId)
                    .MaxAsync(x => x.TimeStamp);

                var notAdded = request.Data.Count();
                request.Data.ToList().ForEach(x =>
                {
                    if (x.Timestamp <= latestMeasurement) return;

                    session.Store(MeasurementDataMapper.Map(x, request));
                    notAdded--;
                });

                await session.SaveChangesAsync();
            }

            return new PersistMeasurementsResponse(true);
        }
    }

    internal static class MeasurementDataMapper
    {
        internal static OS.Data.v1.MeasurementData Map(OS.Dto.v1.Measurement measurement, PersistMeasurementsRequest msg)
        {
            var data = measurement.Data;

            return new OS.Data.v1.MeasurementData()
            {
                DeviceId = msg.DeviceId,
                TimeStamp = measurement.Timestamp,

                Temp = data.Temp,
                Hum = data.Hum,
                Press = data.Press,
                Pm10 = data.Pm10,
                Pm25 = data.Pm25,
                CO = data.CO,
                SO2 = data.SO2,
                NO2 = data.NO2,
                O3 = data.O3,
                Pb = data.Pb,
                Tvoc = data.Tvoc
            };
        }
    }
}