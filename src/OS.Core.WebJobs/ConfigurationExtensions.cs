﻿using System;
using Microsoft.Extensions.Configuration;

namespace OS.Core.WebJobs
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddWhen(this IConfigurationBuilder configuration, Func<bool> predicate,
            Action<IConfigurationBuilder> action)
        {
            if (predicate())
                action(configuration);

            return configuration;
        }
    }
}