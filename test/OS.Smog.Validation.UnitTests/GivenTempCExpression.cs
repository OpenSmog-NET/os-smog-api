using OS.Smog.Dto;
using OS.Smog.Dto.Sensors;
using OS.Smog.Validation.Expressions;
using OS.Smog.Validation.UnitTests.Utils;
using Shouldly;
using Xunit;

namespace OS.Smog.Validation.UnitTests
{
    public class GivenTempCExpression : ExpressionTestFixture
    {
        [Fact]
        public void WhenTempIsInRange_NoError()
        {
            // Arrange
            var expression = new TempCValidationExpression();
            Payload.Add(new Measurement
            {
                Data = new Data { Temp = 45.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(false);
        }

        [Fact]
        public void WhenTempIsLessThenNegative100_Error()

        {
            // Arrange
            var expression = new TempCValidationExpression();
            Payload.Add(new Measurement
            {
                Data = new Data { Temp = -101.0f }
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
            Payload.Add(new Measurement
            {
                Data = new Data { Temp = 101.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }
    }
}