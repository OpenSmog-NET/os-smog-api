using OS.Core.Interpreter;
using OS.Smog.Dto.Sensors;
using System;

namespace OS.Smog.Domain.Sensors.Expressions
{
    public abstract class ConcentrationValidationExpression : ValueRangeValidationExpression<float>, IExpression<PayloadInterpretationContext>
    {
        private const string ConcentrationError = "Concentration must be greater or equal to 0.0 and less or equal than 100.0";
        public abstract string Name { get; }
        public abstract Func<Readings, float?> ValueProvider { get; }

        public abstract string SIUnit { get; }

        public void Interpret(PayloadInterpretationContext context)
        {
            for (var i = 0; i < context.Input.Count; i++)
            {
                var item = context.Input[i];
                var value = ValueProvider(item.Readings);

                if (!value.HasValue) continue;

                if (!ValueIsInRange(value.Value, 0.0f, float.MaxValue))
                {
                    context.Errors.Add($"{item.Timestamp} : {Name} {ConcentrationError} ({value.Value}[{SIUnit}])");
                }
            }
        }
    }
}