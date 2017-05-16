using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace OS.Core.WebJobs
{
    public static class WebJobHostConfiguration
    {
        public static JobHost Configure(IConfiguration configuration, IServiceProvider container,
            bool useServiceBus = false, bool useEventHubs = false, bool useTimers = false)
        {
            var settings = GetSettings(configuration);
            var jobHostConfiguration = CreateJobHostConfiguration(container, settings);

            if (useServiceBus)
            {
                var svcBusConfig = new ServiceBusConfiguration
                {
                    ConnectionString = settings.GetServiceBusConectionString()
                    // MessageOptions = new OnMessageOptions() { MaxConcurrentCalls = 1 }
                };

                jobHostConfiguration.UseServiceBus(svcBusConfig);
            }

            if (useEventHubs)
            {
                var evHubConfig = new EventHubConfiguration();

                jobHostConfiguration.UseEventHub(evHubConfig);
            }

            if (useTimers)
                jobHostConfiguration.UseTimers();

            return new JobHost(jobHostConfiguration);
        }

        private static WebJobSettings GetSettings(IConfiguration configuration)
        {
            var settings = new WebJobSettings(configuration);
            configuration.GetSection("OpenSmog:WebJob").Bind(settings);

            return settings;
        }

        private static JobHostConfiguration CreateJobHostConfiguration(IServiceProvider container,
            WebJobSettings settings)
        {
            var jobHostConfiguration = new JobHostConfiguration
            {
                DashboardConnectionString = settings.GetDashboardConnectionString(),
                StorageConnectionString = settings.GetStorageConnectionString(),
                JobActivator = new WebJobActivator(container)
            };

            return jobHostConfiguration;
        }
    }
}