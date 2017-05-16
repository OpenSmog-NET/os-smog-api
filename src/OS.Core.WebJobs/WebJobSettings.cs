using Microsoft.Extensions.Configuration;

namespace OS.Core.WebJobs
{
    public class WebJobSettings
    {
        private readonly IConfiguration configuration;

        public WebJobSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string DashboardConnectionStringName { get; set; }
        public string StorageConnectionStringName { get; set; }

        public string ServiceBusConnectionStringName { get; set; }

        public string EventHubConnectionStringName { get; set; }

        public string GetDashboardConnectionString() => configuration.GetConnectionString(DashboardConnectionStringName);

        public string GetStorageConnectionString() => configuration.GetConnectionString(StorageConnectionStringName);

        public string GetServiceBusConectionString()
            => configuration.GetConnectionString(ServiceBusConnectionStringName);

        public string GetEventHubConnectionString() => configuration.GetConnectionString(EventHubConnectionStringName);
    }
}