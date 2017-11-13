using OS.Dto;
using System;

namespace OS.Smog.Validation.Expressions
{
    public class PbExpression : ConcentrationValidationExpression
    {
        public override string Name => "Pb";
        public override string SIUnit => "µg/m³";
        public override Func<Data, double?> ValueProvider => x => x.Pb;
    }
}