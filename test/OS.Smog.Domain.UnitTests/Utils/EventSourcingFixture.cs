using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OS.Smog.Domain.UnitTests
{
    public class EventSourcingFixture : IDisposable
    {
        internal static IndentedTextWriter Indented;
        static TextWriter _writer;

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
