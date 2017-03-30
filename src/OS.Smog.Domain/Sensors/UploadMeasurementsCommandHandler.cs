using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace OS.Smog.Domain.Sensors
{
    public class UploadMeasurementsCommandHandler : IRequestHandler<UploadMeasurementsCommand, HttpStatusCode>
    {
        private readonly ILogger<UploadMeasurementsCommandHandler> logger;
        private readonly IRequestHandler<UploadMeasurementsCommand, HttpStatusCode> inner;

        public UploadMeasurementsCommandHandler(ILogger<UploadMeasurementsCommandHandler> logger,
            IRequestHandler<UploadMeasurementsCommand, HttpStatusCode> inner)
        {
            this.logger = logger;
            this.inner = inner;
        }

        public HttpStatusCode Handle(UploadMeasurementsCommand message)
        {
            logger.LogInformation("Processing: {@message}");
            return HttpStatusCode.OK;
        }
    }
}