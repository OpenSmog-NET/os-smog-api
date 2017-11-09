using OS.Smog.Dto;
using OS.Smog.Dto.Sensors;
using OS.Smog.Validation.Expressions;
using OS.Smog.Validation.UnitTests.Utils;
using Shouldly;
using Xunit;

namespace OS.Smog.Validation.UnitTests
{
    public class GivenPayloadValidationExpression : ExpressionTestFixture
    {
        [Fact]
        public void WhenRequestBodyIsEmpty_Error()
        {
            // Arrange
            var expression = new MeasurementsValidationExpression();

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }

        [Fact]
        public void WhenRequestBodyIsNotEmpty_NoError()
        {
            // Arrange
            var expression = new MeasurementsValidationExpression();
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
        public void WhenRequestBodyIsNull_Error()
        {
            // Arrange
            var expression = new MeasurementsValidationExpression();
            var context = new MeasurementsInterpretationContext(null);

            // Act
            expression.Interpret(context);

            // Assert
            context.HasError.ShouldBe(true);
        }
    }
}