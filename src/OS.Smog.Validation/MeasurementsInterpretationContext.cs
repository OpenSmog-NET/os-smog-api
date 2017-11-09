using OS.Smog.Dto;
using System.Collections.Generic;
using System.Linq;

namespace OS.Smog.Validation
{
    public class MeasurementsInterpretationContext : IInterpretationContext<IList<Measurement>>
    {
        public MeasurementsInterpretationContext(IList<Measurement> input)
        {
            Input = input;
        }

        public bool HasError => Errors.Any();
        public IList<string> Errors { get; } = new List<string>();
        public IList<Measurement> Input { get; }
    }
}