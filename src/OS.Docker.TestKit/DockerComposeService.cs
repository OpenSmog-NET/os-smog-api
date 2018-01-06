﻿using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace OS.Docker.TestKit
{
    public class DockerComposeService
    {
        private Dictionary<string, string> environment;
        private Dictionary<int, int> ports;

        [YamlIgnore]
        public IDictionary<string, string> Environment
        {
            get
            {
                if (environment != null) return environment;

                environment = new Dictionary<string, string>();

                YamlEnvironment.ForEach(x =>
                {
                    var tokens = x.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length != 2)
                    {
                        throw new ArgumentException($"Incorrect ENV variable declaratiom {x}");
                    }

                    environment[tokens[0]] = tokens[1];
                });

                return environment;
            }
        }

        [YamlIgnore]
        public IDictionary<int, int> Ports
        {
            get
            {
                if (ports != null) return ports;

                ports = new Dictionary<int, int>();

                YamlPorts.ForEach(x =>
                {
                    var tokens = x.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length != 2)
                    {
                        throw new ArgumentException($"Incorrect PORT declaratiom {x}");
                    }

                    ports[Convert.ToInt32(tokens[0])] = Convert.ToInt32(tokens[1]);
                });

                return ports;
            }
        }

        /// <summary>
        /// We don't really care what's in here right now.
        /// </summary>
        [YamlMember(Alias = "build")]
        public dynamic YamlBuild { get; set; }

        [YamlMember(Alias = "depends_on")]
        public List<string> YamlDependsOn { get; set; }

        [YamlMember(Alias = "environment")]
        public List<string> YamlEnvironment { get; set; }

        [YamlMember(Alias = "ports")]
        public List<string> YamlPorts { get; set; }

        [YamlMember(Alias = "image")]
        public string YamlImage { get; set; }

        [YamlMember(Alias = "deploy")]
        public dynamic YamlDeploy { get; set; }

        [YamlMember(Alias = "configs")]
        public dynamic YamlConfigs { get; set; }

        [YamlMember(Alias = "networks")]
        public dynamic YamlNetworks { get; set; }
    }
}