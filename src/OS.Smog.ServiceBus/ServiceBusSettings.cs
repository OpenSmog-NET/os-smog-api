using Microsoft.Extensions.Configuration;

namespace OS.Smog.ServiceBus
{
    public class ServiceBusSettings
    {
        private readonly IConfiguration configuration;

        public ServiceBusSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string TopicName => configuration["ServiceBus:TopicName"];
        public string ConnectionStringName => configuration["ServiceBus:ConnectionStringName"];
        public string ConnectionString => configuration[$"ConnectionStrings:{ConnectionStringName}"];
    }
}