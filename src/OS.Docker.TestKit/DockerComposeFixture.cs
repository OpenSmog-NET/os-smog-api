using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable VirtualMemberCallInConstructor

namespace OS.Docker.TestKit
{
    /// <summary>
    /// This fixture uses the project's docker-compose integration in order to launch
    /// and clean-up a test session on the application running in docker-compose.
    /// </summary>
    public abstract class DockerComposeFixture : DisposableFixture
    {
        public virtual bool CleanUp => true;
        protected string WorkingDirectory;

        private readonly Process containerProcess;

        /// <summary>
        /// a FIFO list of docker-compose files which will be merged by docker-compose using the -f option
        /// </summary>
        public abstract IReadOnlyList<string> ComposeFiles { get; }

        protected DockerComposeFixture()
        {
            if (ComposeFiles == null)
            {
                throw new ArgumentNullException(nameof(ComposeFiles));
            }

            var builder = new StringBuilder($"");

            ComposeFiles.ForEach(x => builder.Append($" -f {x}"));

            builder.Append($" up");

            var startInfo = new ProcessStartInfo("docker-compose", builder.ToString())
            {
                WorkingDirectory = this.WorkingDirectory = GetWorkingDirectory(ComposeFiles),
                RedirectStandardOutput = false
            };

            containerProcess = Process.Start(startInfo);

            var started = WaitForContainerInitialization(TimeSpan.FromSeconds(10)).Result;
            if (!started)
            {
                throw new Exception($"The docker-compose project failed to initialize within the allowed time");
            }
        }

        protected abstract Task<bool> WaitForContainerInitialization(TimeSpan timeout);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private static string GetWorkingDirectory(IReadOnlyList<string> composeFiles, string directory = null)
        {
            directory = string.IsNullOrEmpty(directory) ? Directory.GetCurrentDirectory() : directory;

            if (Directory.GetFiles(directory, "*.yml").All(file => composeFiles.Contains(file)))
            {
                return directory;
            }

            var parent = Directory.GetParent(directory);

            if (parent.Parent == null)
            {
                throw new InvalidOperationException(string.Join(",",
                    "Unable to locate the directory containing the docker-compose-files: ", composeFiles));
            }

            return GetWorkingDirectory(composeFiles, parent.FullName);
        }
    }
}