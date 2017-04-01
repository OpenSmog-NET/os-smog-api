using OS.Core.Interpreter;
using OS.Smog.Dto.Sensors;
using System.Collections.Generic;
using System.Linq;

namespace OS.Smog.Domain.Sensors
{
    public class PayloadInterpretationContext : IInterpretationContext<Payload>
    {
        public PayloadInterpretationContext(Payload input)
        {
            Input = input;
        }

        public bool HasError => Errors.Any();
        public IList<string> Errors { get; } = new List<string>();
        public Payload Input { get; }
    }
}