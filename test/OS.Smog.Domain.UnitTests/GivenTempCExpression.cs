using OS.Smog.Domain.Sensors.Expressions;
using OS.Smog.Dto.Sensors;
using Shouldly;
using Xunit;

namespace OS.Smog.Domain.UnitTests
{
    public class GivenTempCExpression : ExpressionTestFixture
    {
        [Fact]
        public void WhenTempIsLessThenNegative100_Error()

        {
            // Arrange
            var expression = new TempCValidationExpression();
            Payload.Add(new Measurement()
            {
                Data = new Data() { Temp = -101.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }

        [Fact]
        public void WhenTempIsMoreThen100_Error()
        {
            // Arrange
            var expression = new TempCValidationExpression();
            Payload.Add(new Measurement()
            {
                Data = new Data() { Temp = 101.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }

        [Fact]
        public void WhenTempIsInRange_NoError()
        {
            // Arrange
            var expression = new TempCValidationExpression();
            Payload.Add(new Measurement()
            {
                Data = new Data() { Temp = 45.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(false);
        }
    }
}