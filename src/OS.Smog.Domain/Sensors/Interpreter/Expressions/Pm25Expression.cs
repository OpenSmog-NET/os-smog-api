using System;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.Sensors.Interpreter.Expressions
{
    public class Pm25Expression : ConcentrationValidationExpression
    {
        public override string Name => "PM25";
        public override string SIUnit => "µg/m³";
        public override Func<Data, float?> ValueProvider => ((x) => x.Pm25);
    }
}