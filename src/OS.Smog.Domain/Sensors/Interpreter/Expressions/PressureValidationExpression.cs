using OS.Core.Interpreter;

namespace OS.Smog.Domain.Sensors.Interpreter.Expressions
{
    public sealed class PressureValidationExpression : ValueRangeValidationExpression<double>,
        IExpression<PayloadInterpretationContext>
    {
        private const string PressError = "Pressure must be greater or equal to 800.0 and less or equal than 1200.0";
        private const float PressMin = 800.0f;
        private const float PressMax = 1200.0f;

        public bool Interpret(PayloadInterpretationContext context)
        {
            for (var i = 0; i < context.Input.Count; i++)
            {
                var item = context.Input[i];
                if (!item.Data.Press.HasValue) continue;

                if (!ValueIsInRange(item.Data.Press.Value, PressMin, PressMax))
                    context.Errors.Add($"{item.Timestamp} : {PressError} ({item.Data.Press.Value}[hPa])");
            }

            return true;
        }
    }
}