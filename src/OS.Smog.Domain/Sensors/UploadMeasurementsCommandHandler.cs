using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OS.Core;
using OS.Smog.Domain.Sensors.Interpreter;
using OS.Smog.Dto.Events;
using OS.Smog.Dto.Sensors;
using ProtoBuf;

namespace OS.Smog.Domain.Sensors
{
    public class UploadMeasurementsCommandHandler : IAsyncRequestHandler<UploadMeasurementsCommand, ApiResult>
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<UploadMeasurementsCommandHandler> logger;
        private readonly EventHubClient client;

        public UploadMeasurementsCommandHandler(
            ILogger<UploadMeasurementsCommandHandler> logger,
            IHttpContextAccessor contextAccessor,
            EventHubClient client)
        {
            this.logger = logger;
            this.contextAccessor = contextAccessor;
            this.client = client;
        }

        public async Task<ApiResult> Handle(UploadMeasurementsCommand message)
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

            if (!result.HasError)
            {
                await client.SendAsync(CreateEventData(message.Payload));
            }

            return result;
        }

        private IEnumerable<EventData> CreateEventData(Payload payload)
        {
            for (var i = 0; i < payload.Count; i++)
            {
                

                var cmd = new PersistMeasurementCommand(Guid.NewGuid(), Guid.NewGuid(), payload[i]);
                var json = JsonConvert.SerializeObject(payload[i]);

                yield return new EventData(Encoding.UTF8.GetBytes(json));
            }
        }
    }
}