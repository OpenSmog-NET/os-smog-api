using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.Events
{
    public enum RebuildStrategy
    {
        All = 0,    // Use all events to rebuild state
        Latest      // Use only the latest event in the stream to rebuild state
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RebuildStrategyAttribute : Attribute
    {
        public RebuildStrategy Strategy { get; }
        public RebuildStrategyAttribute(RebuildStrategy strategy)
        {
            Strategy = strategy;
        }
    }
}
