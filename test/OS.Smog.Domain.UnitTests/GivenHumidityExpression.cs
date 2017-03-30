using OS.Smog.Domain.Sensors.Expressions;
using OS.Smog.Dto.Sensors;
using Shouldly;
using Xunit;

namespace OS.Smog.Domain.UnitTests
{
    public class GivenHumidityExpression : ExpressionTestFixture
    {
        [Fact]
        public void WhenHumidityIsLessThen0_Error()
        {
            // Arrange
            var expression = new HumidityValidationExpression();
            Payload.Add(new Measurement()
            {
                Readings = new Readings() { Hum = -1.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }

        [Fact]
        public void WhenHumidityIsMoreThen100_Error()
        {
            // Arrange
            var expression = new HumidityValidationExpression();
            Payload.Add(new Measurement()
            {
                Readings = new Readings() { Hum = 101.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }

        [Fact]
        public void WhenHumidityIsInRange_NoError()
        {
            // Arrange
            var expression = new HumidityValidationExpression();
            Payload.Add(new Measurement()
            {
                Readings = new Readings() { Hum = 45.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(false);
        }
    }
}