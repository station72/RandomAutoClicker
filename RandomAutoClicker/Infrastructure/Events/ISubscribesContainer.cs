using System;

namespace RandomAutoClicker.Infrastructure.Events
{
    public interface ISubscribesContainer<TArgs>
    {
        void Subscribe(string eventName, Action<TArgs> callback);

        void UnsubscribeAll();
    }
}
