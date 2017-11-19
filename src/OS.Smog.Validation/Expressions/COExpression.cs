using OS.Dto.v1;
using System;

namespace OS.Smog.Validation.Expressions
{
    public class COExpression : ConcentrationValidationExpression
    {
        public override string Name => "CO";
        public override string SIUnit => "mg/m³";
        public override Func<Data, double?> ValueProvider => x => x.CO;
    }
}