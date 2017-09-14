using System;
using System.Threading.Tasks;

namespace Abstracts
{
    public interface IEventEmitter
    {
        Task PublishAsync(string type, object message);
        void SubscribeAsync(string type, Func<object, Task> handler);
    }
}
