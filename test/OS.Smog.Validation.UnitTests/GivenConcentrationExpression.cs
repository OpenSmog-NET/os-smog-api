using OS.Smog.Dto;
using OS.Smog.Dto.Sensors;
using OS.Smog.Validation.Expressions;
using OS.Smog.Validation.UnitTests.Utils;
using Shouldly;
using System.Linq;
using Xunit;

namespace OS.Smog.Validation.UnitTests
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