using OS.Core;

namespace OS.Smog.Api.Data
{
    public class ValidateMeasurementsResponse
    {
        public ValidateMeasurementsResponse(bool success)
        {
            Success = success;
        }

        public ValidateMeasurementsResponse(bool success, ApiResult result)
            : this(success)
        {
            Result = result;
        }

        public bool Success { get; }

        public ApiResult Result { get; }
    }
}