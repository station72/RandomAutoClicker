using System;

namespace RandomAutoClicker.Infrastructure.Events
{
    public interface IEventBroker<TArgs>
    {
        void Subscribe(string eventName, Action<TArgs> callback);

        void Unsubscribe(string eventName, Action<TArgs> callback);

        void Unsubscribe(Action<TArgs> callback);

        void Raise(string eventName, TArgs eventArgs);
    }
}
