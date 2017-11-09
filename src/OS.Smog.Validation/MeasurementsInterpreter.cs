using OS.Smog.Validation.Expressions;

namespace OS.Smog.Validation
{
    public static class MeasurementsInterpreter
    {
        private static readonly IExpression<MeasurementsInterpretationContext>[] Expressions =
        {
            new MeasurementsValidationExpression(),
            new TimeStampValidationExpression(),
            new HumidityValidationExpression(),
            new TempCValidationExpression(),
            new PressureValidationExpression(),
            new COExpression(),
            new PbExpression(),
            new NO2Expression(),
            new O3Expression(),
            new Pm10Expression(),
            new Pm25Expression(),
            new SO2Expression()
        };

        public static void Interpret(MeasurementsInterpretationContext context)
        {
            foreach (var expression in Expressions)
                if (!expression.Interpret(context))
                    break;
        }
    }
}