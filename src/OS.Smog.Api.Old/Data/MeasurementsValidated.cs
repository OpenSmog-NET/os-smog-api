using MediatR;
using OS.Dto;
using System;
using System.Collections.Generic;

namespace OS.Smog.Api.Data
{
    public class MeasurementsValidated : List<Measurement>, INotification
    {
        public MeasurementsValidated(Guid deviceId, IEnumerable<Measurement> data)
        {
            DeviceId = deviceId;
            this.AddRange(data);
        }

        public Guid DeviceId { get; }
    }
}