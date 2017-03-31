using OS.Smog.Dto.Sensors;
using System;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public class COExpression : ConcentrationValidationExpression
    {
        public override string Name => "CO";
        public override string SIUnit => "mg/m³";
        public override Func<Data, float?> ValueProvider => ((x) => x.CO);
    }
}