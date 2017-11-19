using OS.Core;
using OS.Events;

namespace OS.Smog.Api.Data
{
    public class MeasurementsValidationFailed : DomainEvent
    {
        public MeasurementsValidationFailed(ApiResult result)
        {
            Result = result;
        }

        public ApiResult Result { get; }
    }
}