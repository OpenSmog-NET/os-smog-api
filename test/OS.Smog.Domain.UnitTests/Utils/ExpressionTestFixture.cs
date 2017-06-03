using OS.Smog.Domain.Sensors.Interpreter;
using OS.Smog.Dto.Sensors;

namespace OS.Smog.Domain.UnitTests
{
    public abstract class ExpressionTestFixture
    {
        protected ExpressionTestFixture()
        {
            Payload = new Payload();
            Context = new PayloadInterpretationContext(Payload);
        }

        public Payload Payload { get; }
        public PayloadInterpretationContext Context { get; }
    }
}