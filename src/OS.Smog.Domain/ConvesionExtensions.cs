namespace OS.Smog.Domain
{
    internal static class ConvesionExtensions
    {
        internal static double? ToNullableDouble(this float? value)
        {
            return (double?) value;
        }
    }
}
