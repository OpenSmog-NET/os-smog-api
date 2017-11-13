using OS.Events;
using System.Threading.Tasks;

namespace OS.Smog.ServiceBus
{
    public interface ITopicClient
    {
        Task SendAsync<T>(T message) where T : IDomainEvent;
    }
}