using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Vagrantegrate.Factory;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Tests
{
    [TestFixture]
    internal class VagranFileTests
    {
        [Test]
        public void Can_create_vagrant_file()
        {
            //Arrange
            var sut = new VagrantFileDefinition(new VagrantFileFactory());

            sut.SetLocation(@"C:/IntegrationTest/Vagrant/");
            sut.StartFromBox("hashicorp/precise64");

            sut.AddExposedPort(80, 8080);
            sut.AddExposedPort(443, 8443);

            sut.AddShellInlineScript("sudo apt-get update");
            sut.AddShellInlineScript("echo test");
           
            //Act
            sut.Save();

            //Assert
            string result;
            
            using (StreamReader sr = File.OpenText(@"C:/IntegrationTest/Vagrant/VagrantFile"))
            {
                result = sr.ReadToEnd();
            }

            result.Should().BeEquivalentTo(
                "Vagrant.configure(2) do |config|" + Environment.NewLine +
                "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                "config.vm.network \"forwarded_port\", guest: 80, host: 8080" + Environment.NewLine +
                "config.vm.network \"forwarded_port\", guest: 443, host: 8443" + Environment.NewLine +
                "config.vm.provision \"shell\", inline: <<-SHELL" + Environment.NewLine +
                "sudo apt-get update" + Environment.NewLine +
                "echo test" + Environment.NewLine +
                "SHELL" + Environment.NewLine +
                "end");
        }

    }
}
