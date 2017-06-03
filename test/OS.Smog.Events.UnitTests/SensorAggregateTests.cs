﻿using OS.Smog.Dto.Sensors;
using OS.Smog.Events.Sensor;
using OS.Smog.Events.UnitTests.Utils;
using Xunit;

namespace OS.Smog.Events.UnitTests
{
    public class SensorAggregateTests : AggregateTestFixture<SensorAggregate.State>
    {
        public SensorAggregateTests(EventSourcingFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public void GivenNoPreviousEvents_WhenMeasurement_ThenMeasurementRegistered()
        {
            // Arrange
            var measurement = new Measurement { Timestamp = 1493936250, Data = new Data() };

            Given();

            // Act
            When(s => SensorAggregate.Register(s, measurement));

            // Assert
            Then(new MeasurementRegistered(measurement));
        }

        [Fact]
        public void GivenPreviousEvents_WhenMeasurementWithDecrementingTimestamp_ThenNoEvent()
        {
            // Arrange
            var m1 = new Measurement { Timestamp = 1493936250, Data = new Data() };
            var m2 = new Measurement { Timestamp = 1493936251, Data = new Data() };
            
            var m3 = new Measurement { Timestamp = 1493936252, Data = new Data() };
            var m4 = new Measurement { Timestamp = 1493936249, Data = new Data() };

            Given(new MeasurementRegistered(m1));
            Given(new MeasurementRegistered(m2));
            Given(new MeasurementRegistered(m3));

            // Act
            When(s => SensorAggregate.Register(s, m4));

            // Assert
            Then();
        }

        [Fact]
        public void GivenPreviousEvents_WhenMeasurementWithIncrementingTimestamp_ThenMeasurementReceived()
        {
            // Arrange
            var m1 = new Measurement { Timestamp = 1493936250, Data = new Data() };
            var m2 = new Measurement { Timestamp = 1493936251, Data = new Data() };
            
            var m3 = new Measurement { Timestamp = 1493936252, Data = new Data() };
            var m4 = new Measurement { Timestamp = 1493936253, Data = new Data() };            

            Given(new MeasurementRegistered(m1));
            Given(new MeasurementRegistered(m2));
            Given(new MeasurementRegistered(m3));

            // Act
            When(s => SensorAggregate.Register(s, m4));

            // Assert
            Then(new MeasurementRegistered(m4));
        }
    }
}