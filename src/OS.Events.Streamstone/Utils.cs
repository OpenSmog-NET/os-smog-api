using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OS.Events.Streamstone
{
    internal static class Utils
    {
        internal static RebuildStrategy GetRebuildStrategy<TState>(this TState state)
            where TState : BaseState
        {
            var type = state.GetType();
            var attr = type.GetAttribute<RebuildStrategyAttribute>();

            if (attr == null)
            {
                throw new InvalidOperationException();
            }

            return attr.Strategy;
        }

        internal static TAttribute GetAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            return  type.GetCustomAttribute(typeof(TAttribute)) as TAttribute;
        }
    }
}
