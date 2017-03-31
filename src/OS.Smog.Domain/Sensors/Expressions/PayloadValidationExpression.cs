using OS.Core.Interpreter;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public class PayloadValidationExpression : IExpression<PayloadInterpretationContext>
    {
        public bool Interpret(PayloadInterpretationContext context)
        {
            if (context.Input == null)
            {
                context.Errors.Add("Empty request body");

                return false;
            }

            return true;
        }
    }
}