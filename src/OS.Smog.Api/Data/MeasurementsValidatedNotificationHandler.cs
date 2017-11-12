using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using OS.Smog.ServiceBus;

namespace OS.Smog.Api.Data
{
    public class MeasurementsValidatedNotificationHandler : IAsyncNotificationHandler<MeasurementsValidated>
    {
        private readonly ILogger<MeasurementsValidatedNotificationHandler> logger;
        private readonly ITopicClient client;

        public MeasurementsValidatedNotificationHandler(ILogger<MeasurementsValidatedNotificationHandler> logger, ITopicClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        public async Task Handle(MeasurementsValidated notification)
        {
            logger.LogInformation("NotificationHandler fired!!!");

            await client.SendAsync(notification);
        }
    }
}