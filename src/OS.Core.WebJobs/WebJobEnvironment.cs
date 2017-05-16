using System;

namespace OS.Core.WebJobs
{
    public static class WebJobEnvironment
    {
        private const string EnvVariableName = "ASPNETCORE_ENVIRONMENT";

        public static string EnvironmentName => Environment.GetEnvironmentVariable(EnvVariableName);

        public static bool IsDevelopment => CompareEnvName("Development");
        public static bool IsTest => CompareEnvName("Test");

        public static bool IsQA => CompareEnvName("QA");

        public static bool IsProduction => CompareEnvName("Prod") || CompareEnvName("Production");

        private static bool CompareEnvName(string envName)
        {
            return EnvironmentName.Equals(envName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}