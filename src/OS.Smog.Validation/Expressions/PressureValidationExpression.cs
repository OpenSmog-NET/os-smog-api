namespace OS.Smog.Validation.Expressions
{
    public sealed class PressureValidationExpression : ValueRangeValidationExpression<double>,
        IExpression<MeasurementsInterpretationContext>
    {
        private const string PressError = "Pressure must be greater or equal to 800.0 and less or equal than 1200.0";
        private const float PressMin = 800.0f;
        private const float PressMax = 1200.0f;

        public bool Interpret(MeasurementsInterpretationContext context)
        {
            foreach (var item in context.Input)
            {
                if (!item.Data.Press.HasValue) continue;

                if (!ValueIsInRange(item.Data.Press.Value, PressMin, PressMax))
                    context.Errors.Add($"{item.Timestamp} : {PressError} ({item.Data.Press.Value}[hPa])");
            }

            return true;
        }
    }
}