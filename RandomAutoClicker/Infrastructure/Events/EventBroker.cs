using System;
using System.Collections.Generic;

namespace RandomAutoClicker.Infrastructure.Events
{
    public sealed class EventBroker<TArgs> : IEventBroker<TArgs>
    {
        private readonly IDictionary<string, List<Action<TArgs>>> _callbacks;

        public EventBroker()
        {
            _callbacks = new Dictionary<string, List<Action<TArgs>>>();
        }

        public void Subscribe(string eventName, Action<TArgs> callback)
        {
            if (!_callbacks.ContainsKey(eventName))
                _callbacks.Add(eventName, new List<Action<TArgs>>());

            _callbacks[eventName].Add(callback);
        }

        public void Unsubscribe(string eventName, Action<TArgs> callback)
        {
            if (!_callbacks.ContainsKey(eventName))
                return;

            _callbacks[eventName].Remove(callback);
        }

        public void Unsubscribe(Action<TArgs> callback)
        {
            foreach (var eventSubscribes in _callbacks.Values)
            {
                eventSubscribes.Remove(callback);
            }
        }

        public void Raise(string eventName, TArgs eventArgs)
        {
            if (!_callbacks.ContainsKey(eventName))
                return;

            foreach (var callback in _callbacks[eventName])
            {
                callback?.Invoke(eventArgs);
            }
        }
    }
}
