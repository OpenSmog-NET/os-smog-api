namespace OS.Smog.Validation.Expressions
{
    public class TimeStampValidationExpression : IExpression<MeasurementsInterpretationContext>
    {
        private const string TimeStampError = "Timestamp validation failed";

        public bool Interpret(MeasurementsInterpretationContext context)
        {
            if (context.Input.Count == 1) return true;

            for (var i = 0; i < context.Input.Count - 1; i++)
                if (context.Input[i].Timestamp >= context.Input[i + 1].Timestamp)
                {
                    context.Errors.Add(TimeStampError);
                    return false;
                }

            return true;
        }
    }
}