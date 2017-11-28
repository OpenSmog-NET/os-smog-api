using MediatR;
using OS.Dto.v1;
using System;
using System.Collections.Generic;

namespace OS.Smog.Api.Data
{
    public class PersistMeasurementsRequest : IRequest<PersistMeasurementsResponse>
    {
        public PersistMeasurementsRequest(Guid deviceId, IEnumerable<Measurement> data)
        {
            DeviceId = deviceId;
            Data = data;
        }

        public Guid DeviceId { get; }

        public IEnumerable<Measurement> Data { get; }
    }
}