namespace OS.Smog.Validation.Expressions
{
    public sealed class HumidityValidationExpression : ValueRangeValidationExpression<double>,
        IExpression<MeasurementsInterpretationContext>
    {
        private const string HumidityError = "Humidity must be greater or equal to 0.0 and less or equal than 100.0";

        public bool Interpret(MeasurementsInterpretationContext context)
        {
            foreach (var item in context.Input)
            {
                if (!item.Data.Hum.HasValue) continue;

                if (!ValueIsInRange(item.Data.Hum.Value, 0.0f, 100.0f))
                    context.Errors.Add($"{item.Timestamp} : {HumidityError} ({item.Data.Hum.Value}[%])");
            }

            return true;
        }
    }
}