using OS.Core.Interpreter;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public sealed class HumidityValidationExpression : ValueRangeValidationExpression<float>, IExpression<PayloadInterpretationContext>
    {
        private const string HumidityError = "Humidity must be greater or equal to 0.0 and less or equal than 100.0";

        public void Interpret(PayloadInterpretationContext context)
        {
            for (var i = 0; i < context.Input.Count; i++)
            {
                var item = context.Input[i];
                if (!item.Data.Hum.HasValue) continue;

                if (!ValueIsInRange(item.Data.Hum.Value, 0.0f, 100.0f))
                {
                    context.Errors.Add($"{item.Timestamp} : {HumidityError} ({item.Data.Hum.Value}[%])");
                }
            }
        }
    }
}