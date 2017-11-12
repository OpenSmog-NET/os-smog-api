using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OS.Core;
using OS.Smog.Validation;
using System.Threading.Tasks;

namespace OS.Smog.Api.Data
{
    public class ValidateMeasurementsCommandHandler : IAsyncRequestHandler<ValidateMeasurementsCommand, ApiResult>
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

        public async Task<ApiResult> Handle(ValidateMeasurementsCommand command)
        {
            logger.LogInformation("Validating: {@message}", command);

            var ctx = new MeasurementsInterpretationContext(command);

            MeasurementsInterpreter.Interpret(ctx);

            var result = new ApiResult(contextAccessor.HttpContext);

            if (ctx.HasError)
            {
                foreach (var error in ctx.Errors)
                {
                    result.Errors.Add(new ApiError { Type = ApiErrorType.Validation, Message = error });
                    logger.LogWarning(error);
                }
            }
            else
            {
                await mediator.Publish(new MeasurementsValidated(command.Id, command));
            }

            logger.LogInformation(!result.HasError ? "Validated: {@message}" : "Failed to validate: {@message}",
                command);

            return result;
        }
    }
}