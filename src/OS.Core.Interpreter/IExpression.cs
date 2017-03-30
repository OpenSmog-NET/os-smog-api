namespace OS.Core.Interpreter
{
    public interface IExpression<in TContext>
        where TContext : IInterpretationContext
    {
        void Interpret(TContext context);
    }
}