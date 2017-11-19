using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OS.Core;
using OS.Events;
using OS.Smog.Validation;

namespace OS.Smog.Api.Data
{
    public class ValidateMeasurementsCommandHandler : IRequestHandler<ValidateMeasurementsCommand, IDomainEvent>
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<ValidateMeasurementsCommandHandler> logger;

        public ValidateMeasurementsCommandHandler(
            ILogger<ValidateMeasurementsCommandHandler> logger,
            IMediator mediator,
            IHttpContextAccessor contextAccessor)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.contextAccessor = contextAccessor;
        }

        public IDomainEvent Handle(ValidateMeasurementsCommand command)
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

                return new MeasurementsValidationFailed(result);
            }

            return new MeasurementsValidated(command.DeviceId, command.Data);
        }
    }
}