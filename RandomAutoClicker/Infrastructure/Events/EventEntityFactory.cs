namespace RandomAutoClicker.Infrastructure.Events
{
    class EventEntityFactory<TArg> : IEventEntityFactory<TArg>
    {
        public IEventBroker<TArg> GetEventBroker()
        {
            return EventBroker<TArg>.Instance;
        }

        public ISubscribesContainer<TArg> GetSubscribeContainer(IEventBroker<TArg> eventBroker)
        {
            return new SubscribesContainer<TArg>(eventBroker);
        }
    }
}
