using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace OS.Smog.Domain.Sensors
{
    public class UploadMeasurementsCommandHandler : IRequestHandler<UploadMeasurementsCommand, HttpStatusCode>
    {
        private readonly ILogger<UploadMeasurementsCommandHandler> logger;
        //private readonly IRequestHandler<UploadMeasurementsCommand, HttpStatusCode> inner;

        public UploadMeasurementsCommandHandler(ILogger<UploadMeasurementsCommandHandler> logger)
        {
            this.logger = logger;
        }

        public HttpStatusCode Handle(UploadMeasurementsCommand message)
        {
            logger.LogInformation("Processing: {@message}", message);

            var ctx = new PayloadInterpretationContext(message.Payload);

            PayloadInterpreter.Interpret(ctx);

            foreach (var error in ctx.Errors)
            {
                logger.LogWarning(error);
            }

            return ctx.HasError ? HttpStatusCode.BadRequest : HttpStatusCode.OK;
        }
    }
}