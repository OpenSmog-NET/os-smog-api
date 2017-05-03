namespace OS.Smog.Interpreter
{
    public interface IExpression<in TContext>
        where TContext : IInterpretationContext
    {
        /// <summary>
        /// The expression attempts to interpret the context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>true if managed to interpret the context input. false if failed and interpretation needs to be halted</returns>
        bool Interpret(TContext context);
    }
}