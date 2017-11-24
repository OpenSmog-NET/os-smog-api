using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OS.Core;
using OS.Smog.Validation;

namespace OS.Smog.Api.Data
{
    public class ValidateMeasurementsRequestHandler : IRequestHandler<ValidateMeasurementsRequest, MeasurementsValidationResponse>
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<ValidateMeasurementsRequestHandler> logger;

        public ValidateMeasurementsRequestHandler(
            ILogger<ValidateMeasurementsRequestHandler> logger,
            IMediator mediator,
            IHttpContextAccessor contextAccessor)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
        }

        public MeasurementsValidationResponse Handle(ValidateMeasurementsRequest command)
        {
            logger.LogInformation("Validating: {@message}", command);

            var ctx = new MeasurementsInterpretationContext(command.Data);

            MeasurementsInterpreter.Interpret(ctx);

            if (ctx.HasError)
            {
                var result = new ApiResult(contextAccessor.HttpContext);
                foreach (var error in ctx.Errors)
                {
                    result.Errors.Add(new ApiError { Type = ApiErrorType.Validation, Message = error });
                    logger.LogWarning(error);
                }

                return new MeasurementsValidationResponse(false, result);
            }

            return new MeasurementsValidationResponse(true);
        }
    }
}