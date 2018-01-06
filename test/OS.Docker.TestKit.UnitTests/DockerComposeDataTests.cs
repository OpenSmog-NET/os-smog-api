using Shouldly;
using System.IO;
using Xunit;
using YamlDotNet.Serialization;

namespace OS.Docker.TestKit.UnitTests
{
    public class DockerComposeDataTests
    {
        [Theory]
        [InlineData("DockerComposeData.TestData01.yml")]
        [InlineData("DockerComposeData.TestData02.yml")]
        [InlineData("DockerComposeData.TestData03.yml")]
        public void GivenYamlConfiguration_WhenDeserializing_ThenDockerComposeServicesShouldBeDeserialized(string fileName)
        {
            // Arrange
            const string key = "os-smog-api.svc";

            // Act
            var deserializer = new Deserializer();
            var result = deserializer.Deserialize<DockerComposeData>(File.ReadAllText(fileName));

            // Assert
            result[key].ShouldNotBe(null);
            result[key].Environment.Count.ShouldBeGreaterThan(0);
            result[key].Ports.Count.ShouldBeGreaterThan(0);
        }
    }
}