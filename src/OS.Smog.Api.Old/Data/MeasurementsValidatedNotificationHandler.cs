using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OS.Core.Middleware;
using OS.Smog.ServiceBus;
using System;
using System.Threading.Tasks;

namespace OS.Smog.Api.Data
{
    public class MeasurementsValidatedNotificationHandler : IAsyncNotificationHandler<MeasurementsValidated>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<MeasurementsValidatedNotificationHandler> logger;
        private readonly ITopicClient client;

        public MeasurementsValidatedNotificationHandler(
            ILogger<MeasurementsValidatedNotificationHandler> logger,
            IHttpContextAccessor contextAccessor,
            ITopicClient client)
        {
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.client = client;
        }

        public async Task Handle(MeasurementsValidated notification)
        {
            var correlationId =
                Guid.Parse(contextAccessor.HttpContext.Request.Headers[Constants.RequestCorrelation.RequestHeaderName]);

            var @event = new OS.Events.Data.MeasurementsValidated()
            {
                CorrelationId = correlationId,
                DeviceId = notification.DeviceId,
                Measurements = notification.ToArray()
            };

            await client.SendAsync(@event);
        }
    }
}