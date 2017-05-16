using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OS.Core.WebJobs;

namespace OS.Smog.Job
{
    public static class Startup
    {
        public static IConfigurationRoot ReadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{WebJobEnvironment.EnvironmentName}.json", true,
                    true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        public static IServiceProvider ConfigureServices(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();

            return services.BuildServiceProvider();
        }
    }
}