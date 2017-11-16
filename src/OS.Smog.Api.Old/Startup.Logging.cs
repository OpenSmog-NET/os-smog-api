using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;

namespace OS.Smog.Api
{
    public static class StartupLoggingExtensions
    {
        public static ILoggerFactory ConfigureLogging(this ILoggerFactory loggerFactory, IConfiguration configuration)
        {
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