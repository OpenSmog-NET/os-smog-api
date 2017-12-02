using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.Swagger.Model;
using System;
using System.Reflection;

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
                Title = "OpenSmog API (0.0.2)",
                Description =
                    "OpenSmog API data ingress API" +
                    "\nBased on the OpenSmog specification:" +
                    "https://github.com/OpenSmog/AcquisitionAPI/blob/master/api/data-ingress/REST/readme.md" +
                    $"\n\nTime deployed: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ssK}",
                Contact =
                    new Contact
                    {
                        Name = "OpenSmog.NET",
                        Email = "maciej.misztal@opensmog.org",
                        Url = "https://github.com/OpenSmog-NET/os-smog-api"
                    }
            };

            //Determine base path for the application.
            var basePath = AppContext.BaseDirectory;
            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;

            services.ConfigureSwagger(appInfo, $"{basePath}/{assemblyName}.xml");

            return services;
        }

        private static void ConfigureSwagger(this IServiceCollection serviceCollection, Info swaggerInfo,
            string swaggerUiPath)
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