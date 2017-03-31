using OS.Core.Interpreter;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public class TimeStampValidationExpression : IExpression<PayloadInterpretationContext>
    {
        private const string TimeStampError = "Timestamp validation failed";

        public bool Interpret(PayloadInterpretationContext context)
        {
            if (context.Input.Count == 1) return true;

            for (var i = 0; i < context.Input.Count - 1; i++)
            {
                if (context.Input[i].Timestamp >= context.Input[i + 1].Timestamp)
                {
                    context.Errors.Add(TimeStampError);
                    return false;
                }
            }

            return true;
        }
    }
}