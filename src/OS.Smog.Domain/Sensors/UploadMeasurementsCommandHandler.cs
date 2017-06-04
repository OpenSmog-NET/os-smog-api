﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OS.Core;
using OS.Core.Middleware;
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
            logger.LogInformation("Validating: {@message}", message);

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
                logger.LogInformation("Validated: {@message}", message);
                await client.SendAsync(CreateEventData(message.Payload));
            }
            else
            {
                logger.LogInformation("Failed to validate: {@message}", message);

            }

            return result;
        }

        private IEnumerable<EventData> CreateEventData(Payload payload)
        {
            for (var i = 0; i < payload.Count; i++)
            {
                var correlationId = Guid.Parse(contextAccessor.HttpContext.Request.Headers[Constants.RequestCorrelation.RequestHeaderName]);
                var deviceId = Guid.Parse(Regex.Match(contextAccessor.HttpContext.Request.Path.Value, @"([a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12})").Value);
                

                var cmd = new PersistMeasurementCommand(correlationId, deviceId, payload[i]);
                var json = JsonConvert.SerializeObject(cmd);

                yield return new EventData(Encoding.UTF8.GetBytes(json));
            }
        }
    }
}