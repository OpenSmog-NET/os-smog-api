using MediatR;
using OS.Core;
using OS.Smog.Dto;
using System;
using System.Collections.Generic;

namespace OS.Smog.Api.Data
{
    public class PostMeasurementsCommand : List<Measurement>, IRequest<ApiResult>
    {
        public PostMeasurementsCommand(Guid id, IEnumerable<Measurement> data)
        {
            Id = id;

            this.AddRange(data);
        }

        public Guid Id { get; private set; }
    }
}