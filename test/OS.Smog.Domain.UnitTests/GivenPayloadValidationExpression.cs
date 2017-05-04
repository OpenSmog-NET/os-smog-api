using OS.Smog.Domain.Sensors;
using OS.Smog.Domain.Sensors.Interpreter;
using OS.Smog.Domain.Sensors.Interpreter.Expressions;
using OS.Smog.Dto.Sensors;
using Shouldly;
using Xunit;

namespace OS.Smog.Domain.UnitTests
{
    public class GivenPayloadValidationExpression : ExpressionTestFixture
    {
        [Fact]
        public void WhenRequestBodyIsNull_Error()
        {
            // Arrange
            var expression = new PayloadValidationExpression();
            var context = new PayloadInterpretationContext(null);

            // Act
            expression.Interpret(context);

            // Assert
            context.HasError.ShouldBe(true);
        }

        [Fact]
        public void WhenRequestBodyIsEmpty_Error()
        {
            // Arrange
            var expression = new PayloadValidationExpression();

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }

        [Fact]
        public void WhenRequestBodyIsNotEmpty_NoError()
        {
            // Arrange
            var expression = new PayloadValidationExpression();
            Payload.Add(new Measurement()
            {
                Data = new Data() { Hum = 45.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(false);
        }
    }
}