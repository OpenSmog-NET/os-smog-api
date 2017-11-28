using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OS.Smog.Api
{
    public static class StartupMartenExtensions
    {
        public static IServiceCollection AddMarten(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDocumentStore>(DocumentStore.For(cfg =>
            {
                cfg.AutoCreateSchemaObjects = AutoCreate.All;
                cfg.Connection(configuration.GetConnectionString("Database"));
            }));

            return services;
        }
    }
}