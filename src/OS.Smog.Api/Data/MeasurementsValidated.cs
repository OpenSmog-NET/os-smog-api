using MediatR;
using OS.Dto.v1;
using OS.Events;
using System;
using System.Collections.Generic;

namespace OS.Smog.Api.Data
{
    public class MeasurementsValidated : DomainEvent, IRequest<IDomainEvent>
    {
        public MeasurementsValidated(Guid deviceId, IEnumerable<Measurement> data)
        {
            DeviceId = deviceId;
            Data = data;
        }

        public Guid DeviceId { get; }

        public IEnumerable<Measurement> Data { get; }
    }
}