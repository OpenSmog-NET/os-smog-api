using OS.Smog.Dto.Sensors;
using System;

namespace OS.Smog.Validation.Expressions
{
    public class O3Expression : ConcentrationValidationExpression
    {
        public override string Name => "O3";
        public override string SIUnit => "µg/m³";
        public override Func<Data, double?> ValueProvider => x => x.O3;
    }
}