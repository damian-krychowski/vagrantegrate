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
using Vagrantegrate.Factory.Provisioning;
using Vagrantegrate.Tests.Infrastructure;

namespace Vagrantegrate.Tests
{
    [TestFixture]
    internal class VagrantTests
    {
        private IVagrant vagrant;

        [OneTimeSetUp]
        public void Prepare_vagrant_environment()
        {
            vagrant = IntegrationTestEnvironment.Prepare()
                .WithEnvironmentFolder("C:/Vagrant/Orion")
                .WithWily64()
                .WithProvision(provision => provision
                    .WithDockerComposeProvisioning(dockercompose => dockercompose
                        .WithDockerComposeFile(@"C:/Vagrant/docker-compose.yml", "./Orion/docker-compose.yml")))
                .WithNetworking(networking => networking
                    .WithPortForwarded(1026, 1026))
                .Prepare();

            vagrant.Up();
        }

        [Test]
        public void Can_create_vagrant_environment_witf_fiware_orion()
        {
            //Act
            var result = HttpGetRequest("http://localhost:1026/version");

            //Assert
            result.Should().Contain("<orion>").And.Contain("</orion>");
        }

        [OneTimeTearDown]
        public void Destroy_vagrant_environment()
        {
            vagrant.Destroy();
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
