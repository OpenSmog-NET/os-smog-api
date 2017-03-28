using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.Swagger.Model;

namespace OS.Smog.Api
{
    public static class StartupSwaggerExtensions
    {
        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseSwagger();
            appBuilder.UseSwaggerUi();

            return appBuilder;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            // Add help pages for API (Swashbuckle)
            var appInfo = new Info
            {
                Version = "v1",
                Title = "OpenSmog API (0.0.1)",
                Description =
                    "WebApi for uploading IoT measurements of air quality," +
                    "according to OpenSmog specification" +
                    $"\n\nTime deployed: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ssK}",
                Contact =
                    new Contact
                    {
                        Name = "OpenSmog",
                        Email = "maciej.misztal@opensmog.org",
                        Url = "https://github.com/OpenSmog/SmogAPI.NET"
                    }
            };

            //Determine base path for the application.
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;

            services.ConfigureSwagger(appInfo, $"{basePath}\\{assemblyName}.xml");

            return services;
        }

        private static void ConfigureSwagger(this IServiceCollection serviceCollection, Info swaggerInfo, string swaggerUiPath)
        {
            serviceCollection.AddSwaggerGen();

            serviceCollection.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(swaggerInfo);

                //Set the comments path for the swagger json and ui.
                options.IncludeXmlComments(swaggerUiPath);
                options.DescribeAllEnumsAsStrings();
                options.CustomSchemaIds(type => type.Name);
            });
        }
    }
}