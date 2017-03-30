using OS.Smog.Domain.Sensors.Expressions;
using OS.Smog.Dto.Sensors;
using Shouldly;
using Xunit;

namespace OS.Smog.Domain.UnitTests
{
    public class GivenPressureExpression : ExpressionTestFixture
    {
        [Fact]
        public void WhenPressIsLessThen800hPa_Error()

        {
            // Arrange
            var expression = new PressureValidationExpression();
            Payload.Add(new Measurement()
            {
                Readings = new Readings() { Press = 799.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }

        [Fact]
        public void WhenPressIsMoreThen1200_Error()
        {
            // Arrange
            var expression = new PressureValidationExpression();
            Payload.Add(new Measurement()
            {
                Readings = new Readings() { Press = 1201.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }

        [Fact]
        public void WhenPressIsInRange_NoError()
        {
            // Arrange
            var expression = new PressureValidationExpression();
            Payload.Add(new Measurement()
            {
                Readings = new Readings() { Press = 1020.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(false);
        }
    }
}