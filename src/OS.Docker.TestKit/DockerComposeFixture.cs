using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

// ReSharper disable VirtualMemberCallInConstructor

namespace OS.Docker.TestKit
{
    /// <summary>
    /// This fixture uses the project's docker-compose integration in order to launch
    /// and clean-up a test session on the application running in docker-compose.
    /// </summary>
    public abstract class DockerComposeFixture : DisposableFixture
    {
        protected string WorkingDirectory;
        private readonly Process composeProcess;

        protected DockerComposeFixture()
        {
            if (ComposeFiles == null)
            {
                throw new ArgumentNullException(nameof(ComposeFiles));
            }

            var builder = ConfigureComposeProcess();

            builder.Append($" up");

            var startInfo = new ProcessStartInfo("docker-compose", builder.ToString())
            {
                WorkingDirectory = this.WorkingDirectory = GetWorkingDirectory(null, ComposeFiles.ToArray()),
                RedirectStandardOutput = false
            };

            composeProcess = Process.Start(startInfo);

            var started = ValidateComposeStartupAsync(TimeSpan.FromSeconds(10)).Result;
            if (!started)
            {
                throw new Exception($"The docker-compose project failed to initialize within the allowed time");
            }
        }

        public virtual bool CleanUp => true;

        /// <summary>
        /// a FIFO list of docker-compose files which will be merged by docker-compose using the -f option
        /// </summary>
        public abstract IReadOnlyList<string> ComposeFiles { get; }

        /// <summary>
        /// When used on a docker-compose.*.yml file, will parse the services' environment and ports
        /// </summary>
        /// <param name="filename">docker-compose override file</param>
        /// <returns></returns>
        public static DockerComposeData ParseDockerComposeSettings(string filename)
        {
            var deserializer = new Deserializer();
            return deserializer.Deserialize<DockerComposeData>(File.ReadAllText(filename));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                composeProcess?.Dispose();
            }

            var builder = ConfigureComposeProcess();

            builder.Append($" rm --force");

            var startInfo = new ProcessStartInfo("docker-compose", builder.ToString())
            {
                WorkingDirectory = this.WorkingDirectory = GetWorkingDirectory(null, ComposeFiles.ToArray()),
                RedirectStandardOutput = false
            };

            Process.Start(startInfo)
                .WaitForExit();
        }

        protected abstract Task<bool> ValidateComposeStartupAsync(TimeSpan timeout);

        public static string GetFullPath(string fileName)
        {
            return $"{GetWorkingDirectory(null, fileName)}/{fileName}";
        }

        public static string GetWorkingDirectory(string directory = null, params string[] composeFiles)
        {
            directory = string.IsNullOrEmpty(directory) ? Directory.GetCurrentDirectory() : directory;
            var files = Directory.GetFiles(directory, "*.yml").Select(x => Path.GetFileName(x));

            if (files.Intersect(composeFiles).Count() == composeFiles.Count())
            {
                return directory;
            }

            var parent = Directory.GetParent(directory);

            if (parent.Parent == null)
            {
                throw new InvalidOperationException(string.Join(",",
                    "Unable to locate the directory containing the docker-compose-files: ", composeFiles));
            }

            return GetWorkingDirectory(parent.FullName, composeFiles);
        }

        private StringBuilder ConfigureComposeProcess()
        {
            var builder = new StringBuilder();

            ComposeFiles?.ForEach(x => builder.Append($" -f {x}"));

            return builder;
        }
    }
}