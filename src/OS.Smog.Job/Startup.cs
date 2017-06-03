using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OS.Core.WebJobs;

namespace OS.Smog.Job
{
    public class Startup
    {
        public static IConfigurationRoot ReadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

            if (WebJobEnvironment.IsDevelopment)
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{WebJobEnvironment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables();

            return builder.Build();
        }

        public static IServiceProvider ConfigureServices(IConfigurationRoot configuration, WebJobSettings settings)
        {
            var services = new ServiceCollection();

            services
                .AddSingleton(configuration)
                .AddSingleton(settings)
                .AddSingleton<SmogWebJob>();

            ConfigureLogging(services, configuration);

            return services.BuildServiceProvider();
        }

        private static void ConfigureLogging(IServiceCollection services, IConfiguration configuration)
        {
            var loggerFactory = new LoggerFactory();

            services.AddSingleton<ILoggerFactory>(loggerFactory);
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        }
    }
}