﻿using OS.Smog.Dto;
using OS.Smog.Dto.Sensors;
using OS.Smog.Validation.Expressions;
using OS.Smog.Validation.UnitTests.Utils;
using Shouldly;
using Xunit;

namespace OS.Smog.Validation.UnitTests
{
    public class GivenPressureExpression : ExpressionTestFixture
    {
        [Fact]
        public void WhenPressIsInRange_NoError()
        {
            // Arrange
            var expression = new PressureValidationExpression();
            Payload.Add(new Measurement
            {
                Data = new Data { Press = 1020.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(false);
        }

        [Fact]
        public void WhenPressIsLessThen800hPa_Error()

        {
            // Arrange
            var expression = new PressureValidationExpression();
            Payload.Add(new Measurement
            {
                Data = new Data { Press = 799.0f }
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
            Payload.Add(new Measurement
            {
                Data = new Data { Press = 1201.0f }
            });

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }
    }
}