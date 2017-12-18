﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable VirtualMemberCallInConstructor

namespace OS.Docker.TestKit
{
    public abstract class DockerFixture : DisposableFixture
    {
        private Process containerProcess;
        public bool CleanUpAfterTest { get; set; } = true;

        protected DockerFixture()
        {
            CleanUp();

            var builder = new StringBuilder($"run --name {ContainerName}");

            EnvironmentVariables?.ForEach(kvp => builder.Append($" -e {kvp.Key}={kvp.Value}"));
            PortMappings?.ForEach(kvp => builder.Append($" -p {kvp.Key}:{kvp.Value}"));
            VolumeMappings?.ForEach(kvp => builder.Append($" -v {kvp.Key}:{kvp.Value}"));

            builder.Append($" {ContainerImageName}");

            var startInfo = new ProcessStartInfo("docker", builder.ToString())
            {
                RedirectStandardOutput = false
            };

            containerProcess = Process.Start(startInfo);

            var started = WaitForContainerInitialization(TimeSpan.FromSeconds(10)).Result;
            if (!started)
            {
                throw new Exception($"The container {ContainerImageName} failed to initialize within the allowed time");
            }
        }

        public abstract string ContainerImageName { get; }   // Container image name. Example: alpine
        public abstract string ContainerName { get; }        // Container name

        public abstract IReadOnlyDictionary<string, string> EnvironmentVariables { get; }
        public abstract IReadOnlyDictionary<ushort, ushort> PortMappings { get; }
        public abstract IReadOnlyDictionary<string, string> VolumeMappings { get; }

        protected abstract Task<bool> WaitForContainerInitialization(TimeSpan timeout);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                containerProcess?.Dispose();
            }

            if (CleanUpAfterTest)
            {
                CleanUp();
            }

            base.Dispose(disposing);
        }

        private void CleanUp()
        {
            Process.Start("docker", $"stop {ContainerName}")
                .WaitForExit();
            Process.Start("docker", $"rm {ContainerName}")
                .WaitForExit();
        }
    }
}