using Abstracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Implements
{
    public class EventEmitter : IEventEmitter
    {
        private readonly Dictionary<string, Func<object, Task>> _handlers = new Dictionary<string, Func<object, Task>>();
        public Task PublishAsync(string type, object message)
        {
            var currentHandler = _handlers[type];
            return currentHandler(message);
        }

        public void SubscribeAsync(string type, Func<object, Task> handler)
        {
            if(_handlers.ContainsKey(type))
            {
                _handlers[type] = handler;
            }
            else
            {
                _handlers.Add(type, handler);
            }
        }
    }
}
