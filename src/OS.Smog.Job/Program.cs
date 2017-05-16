using OS.Core.WebJobs;

namespace OS.Smog.Job
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = Startup.ReadConfiguration();
            var container = Startup.ConfigureServices(configuration);

            var host = WebJobHostConfiguration.Configure(configuration, container,
                useTimers: false,
                useServiceBus: false,
                useEventHubs: true);

            host.CallAsync(typeof(SmogWebJob).GetMethod("RunAsync"));
            host.RunAndBlock();
        }
    }
}