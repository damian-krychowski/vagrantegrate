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
using Vagrantegrate.Tests.Infrastructure;

namespace Vagrantegrate.Tests
{
    internal class VagrantFileTestsFixture : IUnitTestFixture
    {
        public VagrantFileDefinition Sut { get; private set; }

        public void SetUp()
        {
            Sut = new VagrantFileDefinition(new VagrantFileFactory());
        }

        public void TearDown()
        {
        }

        public string ReadVagrantFileContent(string vagrantFilePath)
        {
            using (var sr = File.OpenText(vagrantFilePath))
            {
                return sr.ReadToEnd();
            }
        }
    }

    [TestFixture]
    internal class VagranFileTests : UnitTest<VagrantFileTestsFixture>
    {
        [Test]
        public void Can_create_basic_vagrant_file()
        {
            //Arrange
            Fixture.Sut.SetLocation(@"C:/IntegrationTest/Vagrant/");
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            //Act
            Fixture.Sut.Save();

            //Assert
            Fixture.ReadVagrantFileContent(@"C:/IntegrationTest/Vagrant/VagrantFile")
                .Should().BeEquivalentTo(
                    "Vagrant.configure(2) do |config|" + Environment.NewLine +
                    "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                    "end");
        }

        [Test]
        public void Can_expose_ports()
        {
            //Arrange
            Fixture.Sut.SetLocation(@"C:/IntegrationTest/Vagrant/");
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.AddExposedPort(10, 8010);
            Fixture.Sut.AddExposedPort(32, 8032);

            //Act
            Fixture.Sut.Save();

            //Assert
            Fixture.ReadVagrantFileContent(@"C:/IntegrationTest/Vagrant/VagrantFile")
                .Should().BeEquivalentTo(
                    "Vagrant.configure(2) do |config|" + Environment.NewLine +
                    "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                    "config.vm.network :forwarded_port, guest: 10, host: 8010" + Environment.NewLine +
                    "config.vm.network :forwarded_port, guest: 32, host: 8032" + Environment.NewLine +
                    "end");
        }

        [Test]
        public void Can_use_inline_shell_scripts()
        {
            //Arrange
            Fixture.Sut.SetLocation(@"C:/IntegrationTest/Vagrant/");
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.AddShellInlineScript("sudo apt-get update");
            Fixture.Sut.AddShellInlineScript("sudo apt-get install nano");

            //Act
            Fixture.Sut.Save();

            //Assert
            Fixture.ReadVagrantFileContent(@"C:/IntegrationTest/Vagrant/VagrantFile")
                .Should().BeEquivalentTo(
                    "Vagrant.configure(2) do |config|" + Environment.NewLine +
                    "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                    "config.vm.provision :shell, inline: <<-SHELL" + Environment.NewLine +
                    "sudo apt-get update" + Environment.NewLine +
                    "sudo apt-get install nano" + Environment.NewLine +
                    "SHELL" + Environment.NewLine +
                    "end");
        }

        [Test]
        public void Can_use_external_shell_scripts()
        {
            //Arrange
            Fixture.Sut.SetLocation(@"C:/IntegrationTest/Vagrant/");
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.AddShellExternalScript("https://example.com/provisioner.sh");
            Fixture.Sut.AddShellExternalScript(@"C:\VagrantScripts\Test.sh");

            //Act
            Fixture.Sut.Save();

            //Assert
            Fixture.ReadVagrantFileContent(@"C:/IntegrationTest/Vagrant/VagrantFile")
                .Should().BeEquivalentTo(
                    "Vagrant.configure(2) do |config|" + Environment.NewLine +
                    "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                    "config.vm.provision :shell, path: \"https://example.com/provisioner.sh\"" + Environment.NewLine +
                    "config.vm.provision :shell, path: \"C:\\VagrantScripts\\Test.sh\"" + Environment.NewLine +
                    "end");
        }

        [Test]
        public void Can_use_docker_compose_files()
        {
            //Arrange
            Fixture.Sut.SetLocation(@"C:/IntegrationTest/Vagrant/");
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.AddDockerComposeFile(@"C:\VagrantScripts\Test1\docker-compose.yml", new LinuxUri("./First/docker-compose.yml"));
            Fixture.Sut.AddDockerComposeFile(@"C:\VagrantScripts\Test2\docker-compose.yml", new LinuxUri("./Second/docker-compose.yml"));

            //Act
            Fixture.Sut.Save();

            //Assert
            Fixture.ReadVagrantFileContent(@"C:/IntegrationTest/Vagrant/VagrantFile")
                .Should().BeEquivalentTo(
                    "Vagrant.configure(2) do |config|" + Environment.NewLine +
                    "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                    "config.vm.provision :file, source: \"C:\\VagrantScripts\\Test1\\docker-compose.yml\", destination: \"./First/docker-compose.yml\"" + Environment.NewLine +
                    "config.vm.provision :file, source: \"C:\\VagrantScripts\\Test2\\docker-compose.yml\", destination: \"./Second/docker-compose.yml\"" + Environment.NewLine +
                    "config.vm.provision :docker" + Environment.NewLine +
                    "config.vm.provision :shell, inline: <<-SHELL" + Environment.NewLine +
                    "sudo apt-get update" + Environment.NewLine +
                    "sudo apt-get install docker-compose -y" + Environment.NewLine +
                    "cd /First && sudo docker-compose up -d" + Environment.NewLine +
                    "cd /Second && sudo docker-compose up -d" + Environment.NewLine +
                    "SHELL" + Environment.NewLine +
                    "end");
        }

        [Test]
        public void Can_use_files()
        {
            //Arrange
            Fixture.Sut.SetLocation(@"C:/IntegrationTest/Vagrant/");
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.AddFile(@"C:\Vagrant\File1", "~\\Copied\\File1");
            Fixture.Sut.AddFile(@"C:\Vagrant\File2", "~\\Copied\\File2");

            //Act
            Fixture.Sut.Save();

            //Assert
            Fixture.ReadVagrantFileContent(@"C:/IntegrationTest/Vagrant/VagrantFile")
                .Should().BeEquivalentTo(
                    "Vagrant.configure(2) do |config|" + Environment.NewLine +
                    "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                    "config.vm.provision :file, source: \"C:\\Vagrant\\File1\", destination: \"~\\Copied\\File1\"" + Environment.NewLine +
                    "config.vm.provision :file, source: \"C:\\Vagrant\\File2\", destination: \"~\\Copied\\File2\"" + Environment.NewLine +
                    "end");
        }
    }
}
