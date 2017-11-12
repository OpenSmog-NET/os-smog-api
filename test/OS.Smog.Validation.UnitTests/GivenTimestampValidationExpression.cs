﻿using OS.Smog.Dto;
using OS.Smog.Validation.Expressions;
using OS.Smog.Validation.UnitTests.Utils;
using Shouldly;
using Xunit;

namespace OS.Smog.Domain.UnitTests
{
    public class GivenTimestampValidationExpression : ExpressionTestFixture
    {
        [Fact]
        public void WhenSingleTimestamp_NoError()
        {
            // Arrange
            var expression = new TimeStampValidationExpression();

            var measurements = new[]
            {
                new Measurement {Timestamp = 1}
            };

            Payload.AddRange(measurements);

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(false);
        }

        [Fact]
        public void WhenTimestampsAreIncrementing_NoError()
        {
            // Arrange
            var expression = new TimeStampValidationExpression();

            var measurements = new[]
            {
                new Measurement {Timestamp = 1},
                new Measurement {Timestamp = 2},
                new Measurement {Timestamp = 3}
            };

            Payload.AddRange(measurements);

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(false);
        }

        [Fact]
        public void WhenTimestampsAreNotIncrementing_Error()
        {
            // Arrange
            var expression = new TimeStampValidationExpression();

            var measurements = new[]
            {
                new Measurement {Timestamp = 1},
                new Measurement {Timestamp = 2},
                new Measurement {Timestamp = 1}
            };

            Payload.AddRange(measurements);

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }

        [Fact]
        public void WhenTimestampsContainDuplicates_Error()
        {
            // Arrange
            var expression = new TimeStampValidationExpression();

            var measurements = new[]
            {
                new Measurement {Timestamp = 1},
                new Measurement {Timestamp = 1},
                new Measurement {Timestamp = 3}
            };

            Payload.AddRange(measurements);

            // Act
            expression.Interpret(Context);

            // Assert
            Context.HasError.ShouldBe(true);
        }
    }
}