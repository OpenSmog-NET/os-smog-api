using System.Linq;

namespace OS.Smog.Validation.Expressions
{
    public class TimeStampValidationExpression : IExpression<MeasurementsInterpretationContext>
    {
        private const string TimeStampError = "Timestamp validation failed";

        public bool Interpret(MeasurementsInterpretationContext context)
        {
            var input = context.Input.ToArray();
            if (context.Input.Count() == 1) return true;

            for (var i = 0; i < input.Length - 1; i++)
            {
                if (input[i].Timestamp < input[i + 1].Timestamp) continue;

                context.Errors.Add(TimeStampError);

                return false;
            }

            return true;
        }
    }
}