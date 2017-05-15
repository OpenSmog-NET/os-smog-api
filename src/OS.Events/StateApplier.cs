namespace OS.Events
{
    /// <summary>
    /// Applies events using poorly performing 'dynamic' approach.
    /// One could IL Emit proper applier ;-)
    /// </summary>
    public static class StateApplier
    {
        public static void Apply<TState>(TState state, IEvent @event)
            where TState : BaseState, new()
        {
            ApplyInternal(state, @event);
        }

        public static void Apply<TState>(TState state, params IEvent[] @events)
            where TState : BaseState, new()
        {
            for (var i = 0; i < @events.Length; i++)
            {
                ApplyInternal(state, @events[i]);
            }
        }

        private static void ApplyInternal<TState>(TState state, IEvent @event)
            where TState : BaseState, new()
        {
            ((dynamic)state).Apply((dynamic)@event);
        }
    }
}