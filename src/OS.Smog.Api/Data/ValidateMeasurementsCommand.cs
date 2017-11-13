using MediatR;
using OS.Core;
using OS.Dto;
using System;
using System.Collections.Generic;

namespace OS.Smog.Api.Data
{
    public class ValidateMeasurementsCommand : List<Measurement>, IRequest<ApiResult>
    {
        public ValidateMeasurementsCommand(Guid deviceId, IEnumerable<Measurement> data)
        {
            DeviceId = deviceId;

            this.AddRange(data);
        }

        public Guid DeviceId { get; }
    }
}