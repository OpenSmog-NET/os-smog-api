using OS.Dto.v1;
using System;

namespace OS.Smog.Validation.Expressions
{
    public class Pm10Expression : ConcentrationValidationExpression
    {
        public override string Name => "PM10";
        public override string SIUnit => "µg/m³";
        public override Func<Data, double?> ValueProvider => x => x.Pm10;
    }
}