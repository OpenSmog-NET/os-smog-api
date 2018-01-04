using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace OS.Docker.TestKit
{
    public class DockerComposeData
    {
        [YamlIgnore]
        public DockerComposeService this[string key] => Services[key];

        [YamlMember(Alias = "version")]
        public string Version { get; set; }

        [YamlMember(Alias = "services")]
        public Dictionary<string, DockerComposeService> Services { get; set; }
    }
}