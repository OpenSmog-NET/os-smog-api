using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OS.Events.Streamstone
{
    internal static class TypeExtensions
    {
        public static EventTypeIdAttribute GetEventTypeId(this Type type)
        {
            var result = type.GetCustomAttribute(typeof(EventTypeIdAttribute)) as EventTypeIdAttribute;

            if (result == null)
            {
                throw new InvalidOperationException($"The event of type {type.Name} must be decorated with [EventTypeId] attribute.");
            }

            return result;
        }
    }
}
