using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using Streamstone;

namespace OS.Events.Streamstone
{
    public static class Invoker
    {
        private const int SliceSize = 1000;

        /// <summary>
        ///     Retrieves the partition
        /// </summary>
        /// <param name="table">The Azure Table Storage</param>
        /// <param name="key">Partitioning key (aggregateId)</param>
        /// <returns></returns>
        public static Partition GetPartition(this CloudTable table, string key)
        {
            return new Partition(table, key);
        }

        /// <summary>
        ///     Retrieves the partition
        /// </summary>
        /// <param name="table">The Azure Table Storage</param>
        /// <param name="key">Partitioning key (aggregateId)</param>
        /// <param name="rowKeyPrefix">Customizable row-key prefix</param>
        /// <returns></returns>
        public static Partition GetPartition(this CloudTable table, string partitionKey, string rowKeyPrefix)
        {
            return new Partition(table, partitionKey, rowKeyPrefix);
        }

        public static Stream GetStream(this Partition partition)
        {
            var streamResult = Stream.TryOpen(partition);

            return streamResult.Found ? streamResult.Stream : new Stream(partition);
        }

        /// <summary>
        ///     Persists a batch of events to storage
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="stream"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public static async Task Invoke<TState>(this Stream stream, Func<TState, IEnumerable<IEvent>> aggregate)
            where TState : BaseState, new()
        {
            var latestState = await stream.ReadLatestAsync<TState>();

            await Stream.WriteAsync(stream, aggregate(latestState).Select(Event).ToArray());
        }

        /// <summary>
        ///     Read all events from the stream in a batched manner. Apply them to a state and return the recreated latest state.
        /// </summary>
        /// <typeparam name="TState">The state</typeparam>
        /// <param name="stream">The event stream from a partition</param>
        /// <returns>Latest aggregate state</returns>
        public static async Task<TState> ReadLatestAsync<TState>(this Stream stream)
            where TState : BaseState, new()
        {
            var idx = 1;
            var state = new TState();
            var rebuildStrategy = state.GetRebuildStrategy();

            if (!(await Stream.TryOpenAsync(stream.Partition)).Found)
                return state;

            switch (rebuildStrategy)
            {
                case RebuildStrategy.Latest:
                    idx = stream.Version;
                    break;
                default:
                    break;
            }

            StreamSlice<EventTableData> slice;

            do
            {                
                slice = await Stream.ReadAsync<EventTableData>(stream.Partition, idx, SliceSize);

                if (slice.HasEvents)
                {
                    StateApplier.Apply(state, slice.Events
                        .Select(ev => EventSerializer.Deserialize(ev.Data, ev.EventType))
                        .ToArray());

                    idx = slice.Events.Last().Version + 1;
                }
                else
                {
                    idx = -1;
                }
            } while (!slice.IsEndOfStream);

            return state;
        }

        private static EventData Event(IEvent @event)
        {
            var ev = new EventTableData
            {
                EventType = @event.GetType().GetEventTypeId().Id,
                Data = EventSerializer.Serialize(@event)
            };

            return new EventData(EventId.From(@event.EventId), EventProperties.From(ev));
        }
    }
}