using OS.Core.Interpreter;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public class TimeStampValidationExpression : IExpression<PayloadInterpretationContext>
    {
        public bool Interpret(PayloadInterpretationContext context)
        {
            for (int i = 0; i < context.Input.Count - 1; i++)
            {
            }

            return true;
        }
    }
}