using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OS.Core;
using OS.Smog.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace OS.Smog.Api.Data
{
    public class ValidateMeasurementsRequestHandler : IRequestHandler<ValidateMeasurementsRequest, ValidateMeasurementsResponse>
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

        public Task<ValidateMeasurementsResponse> Handle(ValidateMeasurementsRequest command, CancellationToken token)
        {
            logger.LogInformation("Validating: {@message}", command);

            var ctx = new MeasurementsInterpretationContext(command.Data);

            MeasurementsInterpreter.Interpret(ctx);

            if (!ctx.HasError)
            {
                return Task.FromResult(new ValidateMeasurementsResponse(true));
            }

            var result = new ApiResult(contextAccessor.HttpContext);
            foreach (var error in ctx.Errors)
            {
                result.Errors.Add(new ApiError { Type = ApiErrorType.Validation, Message = error });
                logger.LogWarning(error);
            }

            return Task.FromResult(new ValidateMeasurementsResponse(false, result));
        }
    }
}