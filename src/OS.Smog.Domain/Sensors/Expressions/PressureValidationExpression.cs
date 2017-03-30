using OS.Core.Interpreter;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public sealed class PressureValidationExpression : ValueRangeValidationExpression<float>, IExpression<PayloadInterpretationContext>
    {
        private const string PressError = "Pressure must be greater or equal to 800.0 and less or equal than 1200.0";
        private const float PressMin = 800.0f;
        private const float PressMax = 1200.0f;

        public void Interpret(PayloadInterpretationContext context)
        {
            for (var i = 0; i < context.Input.Count; i++)
            {
                var item = context.Input[i];
                if (!item.Readings.Press.HasValue) continue;

                if (!ValueIsInRange(item.Readings.Press.Value, PressMin, PressMax))
                {
                    context.Errors.Add($"{item.Timestamp} : {PressError} ({item.Readings.Press.Value}[hPa])");
                }
            }
        }
    }
}