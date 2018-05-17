using System;
using System.Collections.Generic;

namespace RandomAutoClicker.Infrastructure.Events
{
    public class SubscribesContainer<TArgs> : ISubscribesContainer<TArgs>
    {
        private readonly List<Action<TArgs>> _callbacks;
        private readonly IEventBroker<TArgs> _eventBroker;

        public SubscribesContainer(IEventBroker<TArgs> eventBroker)
        {
            _callbacks = new List<Action<TArgs>>();
            _eventBroker = eventBroker;
        }

        public void Subscribe(string eventName, Action<TArgs> callback)
        {
            _eventBroker.Subscribe(eventName, callback);
            _callbacks.Add(callback);
        }

        public void UnsubscribeAll()
        {
            _callbacks.ForEach(_eventBroker.Unsubscribe);
            _callbacks.Clear();
        }
    }
}
