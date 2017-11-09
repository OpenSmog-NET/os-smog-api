using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OS.Core;
using OS.Smog.Validation;

//using OS.Smog.Domain.Sensors.Interpreter;
using System.Threading.Tasks;

namespace OS.Smog.Api.Data
{
    public class PostMeasurementsCommandHandler : IAsyncRequestHandler<PostMeasurementsCommand, ApiResult>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<PostMeasurementsCommandHandler> logger;

        public PostMeasurementsCommandHandler(
            ILogger<PostMeasurementsCommandHandler> logger,
            IHttpContextAccessor contextAccessor)
        {
            this.logger = logger;
            this.contextAccessor = contextAccessor;
        }

        public async Task<ApiResult> Handle(PostMeasurementsCommand command)
        {
            logger.LogInformation("Validating: {@message}", command);

            var ctx = new MeasurementsInterpretationContext(command);

            MeasurementsInterpreter.Interpret(ctx);

            var result = new ApiResult(contextAccessor.HttpContext);

            await Task.FromResult(0); // (!)

            foreach (var error in ctx.Errors)
            {
                result.Errors.Add(new ApiError { Type = ApiErrorType.Validation, Message = error });
                logger.LogWarning(error);
            }

            logger.LogInformation(!result.HasError ? "Validated: {@message}" : "Failed to validate: {@message}",
                command);

            return result;
        }

        //private IEnumerable<EventData> CreateEventData(Measurements payload)
        //{
        //    for (var i = 0; i < payload.Count; i++)
        //    {
        //        var correlationId = Guid.Parse(contextAccessor.HttpContext.Request.Headers[Constants.RequestCorrelation.RequestHeaderName]);
        //        var deviceId = Guid.Parse(Regex.Match(contextAccessor.HttpContext.Request.Path.Value, @"([a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12})").Value);

        //        //var cmd = new PersistMeasurementCommand(correlationId, deviceId, payload[i]);
        //        var json = JsonConvert.SerializeObject(cmd);

        //        yield return new EventData(Encoding.UTF8.GetBytes(json));
        //    }
        //}
    }
}