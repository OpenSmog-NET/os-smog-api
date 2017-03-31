using OS.Smog.Domain.Sensors.Expressions;
using Shouldly;
using System;
using Xunit;

namespace OS.Smog.Domain.UnitTests
{
    public class GivenValueRangeValidationExpression
    {
        [Fact]
        public void WhenValueIsInRange_True()
        {
            // Arrange
            var expression = new ValueRangeValidationExpression<float>();

            // Act
            var result = expression.ValueIsInRange(5.0f, 0.0f, 10.0f);

            // Assert
            result.ShouldBe(true);
        }

        [Fact]
        public void WhenValueIsOutOfRange_False()
        {
            // Arrange
            var expression = new ValueRangeValidationExpression<float>();

            // Act
            var result = expression.ValueIsInRange(0.0f, 5.0f, 10.0f);

            // Assert
            result.ShouldBe(false);
        }

        [Fact]
        public void WhenMinIsGreaterThenMax_ArgumentException()
        {
            Should.Throw<ArgumentException>(() =>
            {
                // Arrange
                var expression = new ValueRangeValidationExpression<float>();

                // Act & Assert
                expression.ValueIsInRange(0.0f, 100.0f, 10.0f);
            });
        }
    }
}