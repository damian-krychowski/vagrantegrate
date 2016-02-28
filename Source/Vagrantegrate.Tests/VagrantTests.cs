using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using FluentAssertions;
using NUnit.Framework;

namespace Vagrantegrate.Tests
{
    [TestFixture]
    internal class VagrantTests
    {
        [Test]
        public void Can_create_vagrant_environment_witf_fiware_orion()
        {
            //Arrange
            var sut = IntegrationTestEnvironment.Prepare();

            //Act
            var vagrant = sut.WithEnvironmentFolder(@"C:\Vagrant\Orion")
                .WithWily64()
                .WithProvision(provision => provision
                    .WithDockerComposeProvisioning(dockercompose => dockercompose
                        .WithDockerComposeFile(@"C:/Vagrant/docker-compose.yml", "./docker-compose.yml")))
                .WithNetworking(networking => networking
                    .WithPortForwarded(1026, 1026))
                .Prepare();

            vagrant.Up();

            //Assert
            var result = HttpGetRequest("http://localhost:1026/version");
            result.Should().Contain("<orion>").And.Contain("</orion>");
        }

        private string HttpGetRequest(string url)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Accept] = "*/*";
                byte[] result = client.DownloadData(url);
                return Encoding.UTF8.GetString(result);
            }
        }
    }
}
