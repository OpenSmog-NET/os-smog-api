﻿namespace OS.Smog.Validation.Expressions
{
    public class MeasurementsValidationExpression : IExpression<MeasurementsInterpretationContext>
    {
        public bool Interpret(MeasurementsInterpretationContext context)
        {
            if (context.Input == null)
            {
                context.Errors.Add("Failed to deserialize request body");
                return false;
            }

            if (context.Input.Count == 0)
                context.Errors.Add("Request body is empty");

            return !context.HasError;
        }
    }
}