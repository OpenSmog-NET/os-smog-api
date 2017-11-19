using OS.Dto.v1;
using System;

namespace OS.Smog.Validation.Expressions
{
    public class NO2Expression : ConcentrationValidationExpression
    {
        public override string Name => "NO2";
        public override string SIUnit => "µg/m³";
        public override Func<Data, double?> ValueProvider => x => x.NO2;
    }
}