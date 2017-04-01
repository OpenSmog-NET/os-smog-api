using MediatR;
using OS.Smog.Dto.Sensors;
using System.Net;

namespace OS.Smog.Domain.Sensors
{
    public class UploadMeasurementsCommand : IRequest<HttpStatusCode>
    {
        public Payload Payload { get; private set; }

        public UploadMeasurementsCommand(Payload payload)
        {
            Payload = payload;
        }
    }
}