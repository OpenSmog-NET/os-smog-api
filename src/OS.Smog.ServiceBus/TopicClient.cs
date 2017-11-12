using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Threading.Tasks;

namespace OS.Smog.ServiceBus
{
    public class TopicClient : ITopicClient
    {
        private readonly Microsoft.ServiceBus.Messaging.TopicClient client;

        public TopicClient(ServiceBusSettings settings)
        {
            CreateTopicIfNotExists(settings);

            client = Microsoft.ServiceBus.Messaging.TopicClient.CreateFromConnectionString(settings.ConnectionString, settings.TopicName);
        }

        private static void CreateTopicIfNotExists(ServiceBusSettings settings)
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(settings.ConnectionString);
            if (namespaceManager.TopicExists(settings.TopicName)) return;

            var topicDescription = new TopicDescription(settings.TopicName);

            namespaceManager.CreateTopic(topicDescription);
        }

        public async Task SendAsync<T>(T message)
        {
            await client.SendAsync(new BrokeredMessage(message));
        }
    }
}