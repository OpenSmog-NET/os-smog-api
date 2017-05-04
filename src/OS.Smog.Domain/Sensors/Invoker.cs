using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streamstone;

namespace OS.Smog.Domain.Sensors
{
    public static class Invoker
    {
        /// <summary>
        /// Write to storage
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="stream"></param>
        /// <param name="events"></param>
        /// <returns></returns>
        public static async Task Invoke<TEvent>(this Stream stream, IEnumerable<TEvent> events)
            where TEvent : class
        {


            await Stream.WriteAsync(stream, events
                .Select(x => new EventData()
                {
                    
                }).ToArray());
        }
    }
}
