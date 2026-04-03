using System;
using System.Collections.Generic;

namespace Base
{
    public class EventBus
    {
        public static EventBus Instance => _instance ?? (_instance = new EventBus());

        private static EventBus _instance;
        private readonly Dictionary<string, List<object>> _signalsCallbacks = new();

        public void Subscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;

            if (_signalsCallbacks.ContainsKey(key))
                _signalsCallbacks[key].Add(callback);
            else
                _signalsCallbacks.Add(key, new List<object>() { callback });
        }

        public void Publish<T>(T signal)
        {
            string key = typeof(T).Name;

            if (_signalsCallbacks.ContainsKey(key))
            {
                foreach (object obj in _signalsCallbacks[key])
                {
                    Action<T> callback = obj as Action<T>;
                    callback?.Invoke(signal);
                }
            }
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;

            if (_signalsCallbacks.ContainsKey(key))
                _signalsCallbacks[key].Remove(callback);
        }
    }
}
