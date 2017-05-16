using System;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.Sensors.Interpreter.Expressions
{
    public class Pm10Expression : ConcentrationValidationExpression
    {
        public override string Name => "PM10";
        public override string SIUnit => "µg/m³";
        public override Func<Data, double?> ValueProvider => x => x.Pm10;
    }
}