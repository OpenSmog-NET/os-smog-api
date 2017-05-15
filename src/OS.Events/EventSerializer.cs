using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ProtoBuf;

namespace OS.Events
{
    public static class EventSerializer
    {
        private static readonly IReadOnlyDictionary<Guid, Type> EventTypeLookup;
        
        static EventSerializer()
        {
            EventTypeLookup = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.TryGetTypes())
                .Where(t => typeof(IEvent).IsAssignableFrom(t)
                            && t != typeof(IEvent)
                            && t != typeof(Event))
                .ToDictionary(t => t.GetCustomAttribute<EventTypeIdAttribute>().Id, t => t);
        }

        public static Type[] TryGetTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch
            {
                return Array.Empty<Type>();
            }
        }

        public static byte[] Serialize(IEvent e)
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, e);
                return ms.ToArray();
            }
        }

        public static IEvent Deserialize(byte[] data, Guid eventTypeId)
        {
            return (IEvent) Serializer.Deserialize(EventTypeLookup[eventTypeId], new MemoryStream(data));
        }
    }
}