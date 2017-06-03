using System;

namespace OS.Smog.Domain.Sensors.Interpreter.Expressions
{
    public class ValueRangeValidationExpression<T>
        where T : struct, IComparable
    {
        public bool ValueIsInRange(T value, T min, T max)
        {
            if (min.CompareTo(max) > 0)
                throw new ArgumentException("Min > Max");

            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }
    }
}