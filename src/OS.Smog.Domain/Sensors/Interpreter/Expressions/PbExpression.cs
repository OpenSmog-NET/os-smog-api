using System;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.Sensors.Interpreter.Expressions
{
    public class PbExpression : ConcentrationValidationExpression
    {
        public override string Name => "Pb";
        public override string SIUnit => "µg/m³";
        public override Func<Data, double?> ValueProvider => x => x.Pb;
    }
}