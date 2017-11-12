using Microsoft.Extensions.DependencyInjection;

namespace OS.Smog.ServiceBus
{
    public static class Startup
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services)
        {
            services.AddSingleton<ServiceBusSettings>();
            services.AddSingleton<ITopicClient, TopicClient>();

            return services;
        }
    }
}