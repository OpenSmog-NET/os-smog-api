using System;
using System.Collections;
using OS.Core.WebJobs;

namespace OS.Smog.Job
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var configuration = Startup.ReadConfiguration();
            var webJobSettings = WebJobHostConfiguration.GetSettings(configuration);

            var container = Startup.ConfigureServices(configuration, webJobSettings);
            
            var host = WebJobHostConfiguration.Configure(configuration, container)
                .UseEventHubs((c) =>
                {
                    c.AddReceiver(SmogWebJob.EventHubName,
                        webJobSettings.GetEventHubConnectionString(),
                        webJobSettings.GetStorageConnectionString());
                })
                .Build();

           
                host.CallAsync(typeof(SmogWebJob).GetMethod("RunAsync"));
                host.RunAndBlock();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
                {
                    Console.WriteLine($"{de.Key} : {de.Value}");
                }
            }
        }
    }
}