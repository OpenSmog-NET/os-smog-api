using OS.Smog.Dto.Sensors;
using System;

namespace OS.Smog.Validation.Expressions
{
    public abstract class ConcentrationValidationExpression : ValueRangeValidationExpression<double>,
        IExpression<MeasurementsInterpretationContext>
    {
        private const string ConcentrationError =
            "Concentration must be greater or equal to 0.0 and less or equal than 100.0";

        public abstract string Name { get; }
        public abstract Func<Data, double?> ValueProvider { get; }

        public abstract string SIUnit { get; }

        public bool Interpret(MeasurementsInterpretationContext context)
        {
            for (var i = 0; i < context.Input.Count; i++)
            {
                var item = context.Input[i];
                var value = ValueProvider(item.Data);

                if (!value.HasValue) continue;

                if (!ValueIsInRange(value.Value, 0.00, double.MaxValue))
                    context.Errors.Add($"{item.Timestamp} : {Name} {ConcentrationError} ({value.Value}[{SIUnit}])");
            }

            return true;
        }
    }
}