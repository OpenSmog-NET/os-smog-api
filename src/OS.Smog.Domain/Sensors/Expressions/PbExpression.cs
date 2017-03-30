using OS.Smog.Dto.Sensors;
using System;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public class PbExpression : ConcentrationValidationExpression
    {
        public override string Name => "Pb";
        public override string SIUnit => "µg/m³";
        public override Func<Readings, float?> ValueProvider => ((r) => r.Pb);
    }
}