using OS.Smog.Domain.Sensors;
using OS.Smog.Domain.Sensors.Interpreter;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.UnitTests
{
    public abstract class ExpressionTestFixture
    {
        public Payload Payload { get; }
        public PayloadInterpretationContext Context { get; }

        protected ExpressionTestFixture()
        {
            Payload = new Payload();
            Context = new PayloadInterpretationContext(Payload);
        }
    }
}