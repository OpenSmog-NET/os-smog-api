using System.Collections.Generic;

namespace OS.Smog.Validation
{
    public interface IInterpretationContext
    {
        bool HasError { get; }
        IList<string> Errors { get; }
    }

    public interface IInterpretationContext<TInput> : IInterpretationContext
        where TInput : class
    {
        TInput Input { get; }
    }

    public interface IInterpretationContext<TInput, TOutput> : IInterpretationContext
        where TInput : class
        where TOutput : class, new()
    {
        TInput Input { get; }
        TOutput Output { get; }
    }
}