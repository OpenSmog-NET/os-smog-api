using MediatR;
using OS.Smog.Dto;
using System;
using System.Collections.Generic;

namespace OS.Smog.Api.Data
{
    public class MeasurementsValidated : List<Measurement>, INotification
    {
        public MeasurementsValidated(Guid correlationId, IEnumerable<Measurement> data)
        {
            CorrelationId = correlationId;
            this.AddRange(data);
        }

        public Guid CorrelationId { get; }
    }
}