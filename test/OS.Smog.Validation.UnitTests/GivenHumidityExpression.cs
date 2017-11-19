using OS.Dto.v1;
using OS.Smog.Validation.Expressions;
using OS.Smog.Validation.UnitTests.Utils;
using Shouldly;
using Xunit;

namespace OS.Smog.Validation.UnitTests
{
    public class GivenHumidityExpression : ExpressionTestFixture
    {
        [Fact]
        public void WhenHumidityIsInRange_NoError()
        {
            // Arrange
            var expression = new HumidityValidationExpression();
            Payload.Add(new Measurement
            {
                Data = new Data { Hum = 45.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(false);
        }

        [Fact]
        public void WhenHumidityIsLessThen0_Error()
        {
            // Arrange
            var expression = new HumidityValidationExpression();
            Payload.Add(new Measurement
            {
                Data = new Data { Hum = -1.0f }
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
            Payload.Add(new Measurement
            {
                Data = new Data { Hum = 101.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }
    }
}