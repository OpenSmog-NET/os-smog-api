using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace OS.Core.WebJobs
{
    public static class WebJobHostConfiguration
    {
        public static JobHostConfiguration Configure(IConfiguration configuration, IServiceProvider container,
            bool useTimers = false)
        {
            var settings = GetSettings(configuration);
            var jobHostConfiguration = CreateJobHostConfiguration(container, settings);
           
            if (useTimers)
                jobHostConfiguration.UseTimers();

            return jobHostConfiguration;
        }

        public static JobHostConfiguration UseServiceBus(this JobHostConfiguration jobHostConfiguration, IConfiguration configuration)
        {
            var settings = GetSettings(configuration);

            var svcBusConfig = new ServiceBusConfiguration
            {
                ConnectionString = settings.GetServiceBusConectionString()
            };

            jobHostConfiguration.UseServiceBus(svcBusConfig);

            return jobHostConfiguration;            
        }

        public static JobHostConfiguration UseEventHubs(this JobHostConfiguration jobHostConfiguration, 
            params Action<EventHubConfiguration>[] addReceiverActions)
        {
            var evHubConfig = new EventHubConfiguration();

            foreach (var action in addReceiverActions)
            {
                action(evHubConfig);
            }

            jobHostConfiguration.UseEventHub(evHubConfig);

            return jobHostConfiguration;            
        }

        public static JobHost Build(this JobHostConfiguration configuration)
        {
            return new JobHost(configuration);
        }

        public static WebJobSettings GetSettings(IConfiguration configuration)
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