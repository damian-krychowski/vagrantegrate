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
    [Explicit]
    [TestFixture]
    internal class VagrantTests
    {
        private IVagrant _vagrant;

        [OneTimeSetUp]
        public void Prepare_vagrant_environment()
        {
            _vagrant = IntegrationTestEnvironment.Prepare()
                .WithEnvironmentFolder("C:/Vagrant/Orion")
                .WithWily64()
                .UseVirtualBox(virtualBox => virtualBox
                    .WithMemory(2048)
                    .WithCpus(2)
                    .WithVmName("VagrantForOrion"))
                .WithProvision(provision => provision
                    .WithDockerComposeProvisioning(dockercompose => dockercompose                   
                        .WithDockerComposeFile(orion => orion
                            .From(@"C:/Vagrant/OrionDocker/docker-compose.yml")
                            .To("./Orion/docker-compose.yml")
                            .IncludeContainingFolder())))
                .WithNetworking(networking => networking
                    .WithPortForwarded(1026, 1026))
                .CheckAndPrepare();

            _vagrant.Destroy();
            _vagrant.Up();
        }

        [Test]
        public void Can_create_vagrant_environment_witf_fiware_orion()
        {
            //Act
            var result = HttpGetRequest("http://localhost:1026/version");

            //Assert
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
