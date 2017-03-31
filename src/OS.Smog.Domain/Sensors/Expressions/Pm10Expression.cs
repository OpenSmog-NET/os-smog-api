using OS.Smog.Dto.Sensors;
using System;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public class Pm10Expression : ConcentrationValidationExpression
    {
        public override string Name => "PM10";
        public override string SIUnit => "µg/m³";
        public override Func<Data, float?> ValueProvider => ((r) => r.Pm10);
    }
}