using System.Threading.Tasks;

namespace OS.Smog.ServiceBus
{
    public interface ITopicClient
    {
        Task SendAsync<T>(T message);
    }
}