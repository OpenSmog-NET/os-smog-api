using System;
using System.CodeDom.Compiler;
using System.IO;

namespace OS.Smog.Domain.UnitTests
{
    public class EventSourcingFixture : IDisposable
    {
        internal static IndentedTextWriter Indented;
        private static TextWriter _writer;

        public EventSourcingFixture()
        {
            _writer = Console.Out;
            Indented = new IndentedTextWriter(_writer);
            Console.SetOut(Indented);
        }

        public void Dispose()
        {
            Console.SetOut(_writer);
        }
    }
}