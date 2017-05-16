using OS.Core.Interpreter;

namespace OS.Smog.Domain.Sensors.Interpreter.Expressions
{
    public sealed class TempCValidationExpression : ValueRangeValidationExpression<double>,
        IExpression<PayloadInterpretationContext>
    {
        private const string TempError = "Temperature must be greater or equal to -100.0 and less or equal than 100.0";
        private const float TempMin = -100.0f;
        private const float TempMax = 100.0f;

        public bool Interpret(PayloadInterpretationContext context)
        {
            for (var i = 0; i < context.Input.Count; i++)
            {
                var item = context.Input[i];
                if (!item.Data.Temp.HasValue) continue;

                if (!ValueIsInRange(item.Data.Temp.Value, TempMin, TempMax))
                    context.Errors.Add($"{item.Timestamp} : {TempError} ({item.Data.Temp.Value}[C])");
            }

            return true;
        }
    }
}