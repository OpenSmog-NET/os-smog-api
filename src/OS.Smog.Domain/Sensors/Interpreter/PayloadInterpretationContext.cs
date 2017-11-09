﻿using OS.Core.Interpreter;
using OS.Smog.Dto;
using System.Collections.Generic;
using System.Linq;

namespace OS.Smog.Domain.Sensors.Interpreter
{
    public class PayloadInterpretationContext : IInterpretationContext<IList<Measurement>>
    {
        public PayloadInterpretationContext(IList<Measurement> input)
        {
            Input = input;
        }

        public bool HasError => Errors.Any();
        public IList<string> Errors { get; } = new List<string>();
        public IList<Measurement> Input { get; }
    }
}