namespace OS.Smog.Api.Data
{
    public class PersistMeasurementsResponse
    {
        public PersistMeasurementsResponse(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}