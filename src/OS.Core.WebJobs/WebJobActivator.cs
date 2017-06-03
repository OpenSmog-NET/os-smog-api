using System;
using Microsoft.Azure.WebJobs.Host;

namespace OS.Core.WebJobs
{
    /// <summary>
    /// The WebJob's Adapter to the udnerlying DI container
    /// </summary>
    public class WebJobActivator : IJobActivator
    {
        private readonly IServiceProvider container;

        public WebJobActivator(IServiceProvider container)
        {
            this.container = container;
        }

        public T CreateInstance<T>()
        {
            return (T)container.GetService(typeof(T));
        }
    }
}