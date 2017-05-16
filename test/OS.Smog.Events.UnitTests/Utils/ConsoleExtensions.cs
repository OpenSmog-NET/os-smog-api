using System;

namespace OS.Smog.Events.UnitTests.Utils
{
    public static class ConsoleExtensions
    {
        public static IDisposable Indent()
        {
            const int value = 1;

            EventSourcingFixture.Indented.Indent += value;

            return new Disposable(() => EventSourcingFixture.Indented.Indent -= value);
        }

        public class Disposable : IDisposable
        {
            private readonly Action a;

            public Disposable(Action a)
            {
                this.a = a;
            }

            public void Dispose()
            {
                a();
            }
        }
    }
}