using System;
using System.Threading.Tasks;

namespace OS.Docker.TestKit
{
    /// <summary>
    /// Container startup validators ensure that containers have initialized successfully within the allowed time range
    /// </summary>
    public interface IContainerStartupValidator
    {
        Task<bool> ValidateContainerStartupAsync(TimeSpan timeout, object state);
    }
}