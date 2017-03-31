using OS.Core.Interpreter;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public class PayloadValidationExpression : IExpression<PayloadInterpretationContext>
    {
        public bool Interpret(PayloadInterpretationContext context)
        {
            if (context.Input == null)
            {
                context.Errors.Add("Failed to deserialize request body");
                return false;
            }

            if (context.Input.Count == 0)
            {
                context.Errors.Add("Request body is empty");
            }

            return !context.HasError;
        }
    }
}