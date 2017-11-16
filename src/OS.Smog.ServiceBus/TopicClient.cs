using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.InteropExtensions;
using OS.Events;
using System.IO;
using System.Threading.Tasks;

namespace OS.Smog.ServiceBus
{
    public class TopicClient : ITopicClient
    {
        private readonly Microsoft.Azure.ServiceBus.ITopicClient client;

        public TopicClient(ServiceBusSettings settings)
        {
            client = new Microsoft.Azure.ServiceBus.TopicClient(settings.ConnectionString, settings.TopicName);
        }

        public async Task SendAsync<T>(T message)
            where T : IDomainEvent
        {
            await client.SendAsync(Serialize(message));
        }

        private static Message Serialize<T>(T message)
        {
            using (var stream = new MemoryStream())
            {
                DataContractBinarySerializer<T>.Instance.WriteObject(stream, message);
                return new Message(stream.ToArray());
            }
        }
    }
}