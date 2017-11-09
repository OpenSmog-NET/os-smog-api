using OS.Smog.Domain.Sensors.Interpreter;
using OS.Smog.Dto;

namespace OS.Smog.Domain.UnitTests
{
    public abstract class ExpressionTestFixture
    {
        protected ExpressionTestFixture()
        {
            Payload = new Measurements();
            Context = new PayloadInterpretationContext(Payload);
        }

        public Measurements Payload { get; }
        public PayloadInterpretationContext Context { get; }
    }
}