using System;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.Sensors.Interpreter.Expressions
{
    public class O3Expression : ConcentrationValidationExpression
    {
        public override string Name => "O3";
        public override string SIUnit => "µg/m³";
        public override Func<Data, double?> ValueProvider => ((x) => x.O3);
    }
}