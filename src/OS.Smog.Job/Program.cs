using OS.Core.WebJobs;

namespace OS.Smog.Job
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = Startup.ReadConfiguration();
            var container = Startup.ConfigureServices(configuration);

            var webJobSettings = WebJobHostConfiguration.GetSettings(configuration);
            var host = WebJobHostConfiguration.Configure(configuration, container)
                .UseEventHubs((c) => c.AddReceiver("os-smog-api-measurements", webJobSettings.GetEventHubConnectionString()))
                .Build();

            host.CallAsync(typeof(SmogWebJob).GetMethod("RunAsync"));
            host.RunAndBlock();
        }
    }
}