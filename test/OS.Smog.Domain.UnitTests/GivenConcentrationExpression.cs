using System.Linq;
using OS.Smog.Domain.Sensors.Interpreter.Expressions;
using OS.Smog.Dto.Sensors;
using Shouldly;
using Xunit;

namespace OS.Smog.Domain.UnitTests
{
    public class GivenConcentrationExpression : ExpressionTestFixture
    {
        [Fact]
        public void WhenConcentrationExpressionValueIsInRange_NoError()
        {
            // Arrange
            var expression = new COExpression();
            Payload.Add(new Measurement
            {
                Data = new Data { CO = 10.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(false);
        }

        [Fact]
        public void WhenConcentrationExpressionValueIsOutOfRange_Error()
        {
            // Arrange
            var expression = new COExpression();
            Payload.Add(new Measurement
            {
                Data = new Data { CO = -10.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
            Context.Errors.First().ShouldContain(expression.Name);
        }
    }
}