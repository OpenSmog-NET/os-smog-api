using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OS.Smog.Domain;

namespace OS.Smog.Api
{
    /// <summary>
    ///     Startup Extensions for setting up the MediatR
    /// </summary>
    public static class StartupMediatrExtensions
    {
        /// <summary>
        ///     Configures the Mediator. Automatically registers the IRequestHandlers&lt;,&gt;
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<SingleInstanceFactory>(sp => t => sp.GetService(t));
            services.AddTransient<MultiInstanceFactory>(sp => t => sp.GetServices(t));
            return services.AddMediatorHandlers(typeof(DomainModule).GetTypeInfo().Assembly);
        }

        private static IServiceCollection AddMediatorHandlers(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (
                    var handlerType in
                    interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                )
                    services.AddTransient(handlerType.AsType(), type.AsType());

                foreach (
                    var handlerType in
                    interfaces.Where(
                        i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAsyncRequestHandler<,>)))
                    services.AddTransient(handlerType.AsType(), type.AsType());
            }

            return services;
        }
    }
}