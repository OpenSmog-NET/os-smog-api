using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace OS.Smog.Api
{
    public static class StartupLoggingExtensions
    {
        public static ILoggerFactory ConfigureLogging(this ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                //.WriteTo.Logentries("85a62b8b-b6e0-46e7-8432-58afa1f619a8")
                .CreateLogger();

            loggerFactory.AddSerilog();

            return loggerFactory;
        }
    }
}