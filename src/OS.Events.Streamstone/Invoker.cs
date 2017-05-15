using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Streamstone;

namespace OS.Events.Streamstone
{
    public static class Invoker
    {
        private const int SliceSize = 1000;

        public static Partition GetPartition(this CloudTable table, string key)
        {
            return new Partition(table, key);
        }

        public static Partition GetPartition(this CloudTable table, string partitionKey, string rowKeyPrefix)
        {
            return new Partition(table, partitionKey, rowKeyPrefix);
        }

        public static Stream GetStream(this Partition partition)
        {
            var streamResult = Stream.TryOpen(partition);

            return streamResult.Found ? streamResult.Stream : new Stream(partition);
        }

        public static async Task Invoke<TState>(this Stream stream, Func<TState, IEnumerable<IEvent>> aggregate)
            where TState : BaseState, new()
        {
            var latestState = await stream.ReadLatestAsync<TState>();

            await Stream.WriteAsync(stream, aggregate(latestState).Select(Event).ToArray());
        }

        public static async Task<TState> ReadLatestAsync<TState>(this Stream stream)
            where TState : BaseState, new()
        {
            var idx = 1;                        
            var state = new TState();

            if (!(await Stream.TryOpenAsync(stream.Partition)).Found)
            {
                return state;
            }

            StreamSlice<EventTableData> slice;

            do
            {
                slice = await Stream.ReadAsync<EventTableData>(stream.Partition, idx , SliceSize);

                if (slice.HasEvents)
                {
                    foreach (var ev in slice.Events)
                    {
                        StateApplier.Apply(state, EventSerializer.Deserialize(ev.Data, ev.EventType));
                    }

                    idx = slice.Events.Last().Version + 1;
                }
                else
                {
                    idx = -1;
                }                
            }
            while(!slice.IsEndOfStream);

            return state;
        }

        private static EventData Event(IEvent @event)
        {
            var ev = new EventTableData()
            {
                EventType = @event.GetType().GetEventTypeId().Id,
                Data = EventSerializer.Serialize(@event)
            };

            return new EventData(EventId.From(@event.EventId), EventProperties.From(ev));
        }       
    }
}