using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;

namespace OS.Smog.Api
{
    public static class StartupLoggingExtensions
    {
        public static ILoggerFactory ConfigureLogging(this ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            var sinks = configuration.GetSection("Serilog").GetSection("WriteTo").GetChildren();
            foreach (var sink in sinks)
            {
                Console.WriteLine(sink["Name"]);
                var args = sink.GetSection("Args").GetChildren();
                foreach (var arg in args)
                {
                    Console.WriteLine($"{arg.Key} : {arg.Value} [{arg.Value.Length} chars]");
                }
            }

            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .CreateLogger();

            loggerFactory.AddSerilog();

            return loggerFactory;
        }
    }
}