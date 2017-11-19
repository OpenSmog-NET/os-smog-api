using OS.Dto.v1;
using System.Collections.Generic;
using System.Linq;

namespace OS.Smog.Validation
{
    public class MeasurementsInterpretationContext : IInterpretationContext<IEnumerable<Measurement>>
    {
        public MeasurementsInterpretationContext(IEnumerable<Measurement> input)
        {
            Input = input;
        }

        public bool HasError => Errors.Any();
        public IList<string> Errors { get; } = new List<string>();
        public IEnumerable<Measurement> Input { get; }
    }
}