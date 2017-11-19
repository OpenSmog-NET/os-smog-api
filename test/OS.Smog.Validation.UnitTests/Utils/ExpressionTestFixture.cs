using OS.Dto.v1;

namespace OS.Smog.Validation.UnitTests.Utils
{
    public abstract class ExpressionTestFixture
    {
        protected ExpressionTestFixture()
        {
            Payload = new Measurements();
            Context = new MeasurementsInterpretationContext(Payload);
        }

        public Measurements Payload { get; }
        public MeasurementsInterpretationContext Context { get; }
    }
}