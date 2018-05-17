namespace RandomAutoClicker.Infrastructure.Events
{
    public interface IEventEntityFactory<TArg>
    {
        IEventBroker<TArg> GetEventBroker();

        ISubscribesContainer<TArg> GetSubscribeContainer(IEventBroker<TArg> eventBroker);
    }
}
