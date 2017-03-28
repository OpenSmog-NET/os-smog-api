using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace OS.Smog.Domain.Sensors
{
    public class UploadMeasurementsCommandHandler : IRequestHandler<UploadMeasurementsCommand, HttpStatusCode>
    {
        private readonly ILogger<UploadMeasurementsCommandHandler> logger;

        public UploadMeasurementsCommandHandler(ILogger<UploadMeasurementsCommandHandler> logger)
        {
            this.logger = logger;
        }

        public HttpStatusCode Handle(UploadMeasurementsCommand message)
        {
            logger.LogInformation("Processing: {@message}");
            return HttpStatusCode.OK;
        }
    }
}