using System;
using System.Collections.Generic;

namespace RandomAutoClicker.Infrastructure.Events
{
    public sealed class EventBroker<TArgs> : IEventBroker<TArgs>
    {
        private static object syncRoot = new Object();
        private readonly Dictionary<string, List<Action<TArgs>>> callbacks;

        private EventBroker()
        {
            callbacks = new Dictionary<string, List<Action<TArgs>>>();
        }

        private static EventBroker<TArgs> _instance;
        public static EventBroker<TArgs> Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new EventBroker<TArgs>();
                        }
                    }
                }

                return _instance;
            }
        }

        public void Subscribe(string eventName, Action<TArgs> callback)
        {
            if (!callbacks.ContainsKey(eventName))
                callbacks.Add(eventName, new List<Action<TArgs>>());

            callbacks[eventName].Add(callback);
        }

        public void Unsubscribe(string eventName, Action<TArgs> callback)
        {
            if (!callbacks.ContainsKey(eventName))
                return;

            callbacks[eventName].Remove(callback);
        }

        public void Unsubscribe(Action<TArgs> callback)
        {
            foreach (var eventSubscribes in callbacks.Values)
            {
                eventSubscribes.Remove(callback);
            }
        }

        public void Raise(string eventName, TArgs eventArgs)
        {
            if (!callbacks.ContainsKey(eventName))
                return;

            foreach (var callback in callbacks[eventName])
            {
                callback?.Invoke(eventArgs);
            }
        }
    }
}
