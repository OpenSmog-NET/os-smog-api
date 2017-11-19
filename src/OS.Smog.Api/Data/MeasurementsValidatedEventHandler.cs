using Marten;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OS.Core.Middleware;
using OS.Core.Queues;
using OS.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Smog.Api.Data
{
    public class MeasurementsValidatedEventHandler : IAsyncRequestHandler<MeasurementsValidated, IDomainEvent>
    {
        private readonly ILogger<MeasurementsValidatedEventHandler> logger;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IDocumentStore store;
        private readonly IQueueClient client;

        public MeasurementsValidatedEventHandler(
                ILogger<MeasurementsValidatedEventHandler> logger,
                IHttpContextAccessor contextAccessor,
                IDocumentStore store,
                IQueueClient client)
        {
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.store = store;
            this.client = client;
        }

        public async Task<IDomainEvent> Handle(MeasurementsValidated message)
        {
            message.CorrelationId = Guid.Parse(contextAccessor.HttpContext.Request.Headers[Constants.RequestCorrelation.RequestHeaderName]);

            //await client.SendAsync(message, "measurements");
            //var data = new OS.Data.v1.MeasurementData()
            //{
            //    //CorrelationId = correlationId,
            //    //DeviceId = notification.DeviceId,
            //    //Measurements = notification.Data.ToArray()
            //};

            using (var session = store.OpenSession())
            {
                session.Store(message.Data.Select(x => MeasurementDataMapper.Map(x, message)));
                session.SaveChanges();
            }

            return null;
        }
    }

    internal static class MeasurementDataMapper
    {
        internal static OS.Data.v1.MeasurementData Map(OS.Dto.v1.Measurement measurement, MeasurementsValidated msg)
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