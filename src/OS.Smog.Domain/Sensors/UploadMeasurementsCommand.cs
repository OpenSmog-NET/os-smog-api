using MediatR;
using OS.Core;
using OS.Smog.Dto;

namespace OS.Smog.Domain.Sensors
{
    public class UploadMeasurementsCommand : IRequest<ApiResult>
    {
        public UploadMeasurementsCommand(Measurements payload)
        {
            Payload = payload;
        }

        public Measurements Payload { get; private set; }
    }
}