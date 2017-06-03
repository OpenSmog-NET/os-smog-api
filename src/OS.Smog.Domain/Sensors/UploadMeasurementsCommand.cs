using MediatR;
using OS.Core;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.Sensors
{
    public class UploadMeasurementsCommand : IRequest<ApiResult>
    {
        public UploadMeasurementsCommand(Payload payload)
        {
            Payload = payload;
        }

        public Payload Payload { get; private set; }
    }
}