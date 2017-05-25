using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OS.Core;
using OS.Smog.Domain.Sensors.Interpreter;

namespace OS.Smog.Domain.Sensors
{
    public class UploadMeasurementsCommandHandler : IRequestHandler<UploadMeasurementsCommand, ApiResult>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<UploadMeasurementsCommandHandler> logger;

        public UploadMeasurementsCommandHandler(
            ILogger<UploadMeasurementsCommandHandler> logger,
            IHttpContextAccessor contextAccessor)
        {
            this.logger = logger;
            this.contextAccessor = contextAccessor;
        }

        public ApiResult Handle(UploadMeasurementsCommand message)
        {
            logger.LogInformation("Processing: {@message}", message);

            var ctx = new PayloadInterpretationContext(message.Payload);

            PayloadInterpreter.Interpret(ctx);

            var result = new ApiResult(contextAccessor.HttpContext);

            foreach (var error in ctx.Errors)
            {
                result.Errors.Add(new ApiError { Type = ApiErrorType.Validation, Message = error });
                logger.LogWarning(error);
            }

            //todo: if no errors --> send to EventHub

            return result;
        }
    }
}