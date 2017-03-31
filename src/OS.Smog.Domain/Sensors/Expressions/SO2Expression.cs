using OS.Smog.Dto.Sensors;
using System;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public class SO2Expression : ConcentrationValidationExpression
    {
        public override string Name => "SO2";
        public override string SIUnit => "µg/m³";
        public override Func<Data, float?> ValueProvider => ((r) => r.SO2);
    }
}