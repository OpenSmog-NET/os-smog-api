using System;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.Sensors.Interpreter.Expressions
{
    public class COExpression : ConcentrationValidationExpression
    {
        public override string Name => "CO";
        public override string SIUnit => "mg/m³";
        public override Func<Data, double?> ValueProvider => x => x.CO;
    }
}