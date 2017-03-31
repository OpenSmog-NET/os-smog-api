using OS.Smog.Dto.Sensors;
using System;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public class NO2Expression : ConcentrationValidationExpression
    {
        public override string Name => "NO2";
        public override string SIUnit => "µg/m³";
        public override Func<Data, float?> ValueProvider => ((x) => x.NO2);
    }
}