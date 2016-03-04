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
using Vagrantegrate.Factory.VagrantFile.Providers;
using Vagrantegrate.Tests.Infrastructure;

namespace Vagrantegrate.Tests
{
    internal class VagrantFileTestsFixture : ITestFixture
    {
        public VagrantFileDefinition Sut { get; private set; }

        public void SetUp()
        {
            Sut = new VagrantFileDefinition();
        }

        public void SaveVagrantFile()
        {
            var saver = new VagrantFileFactory();
            saver.Create(Sut.ToString(), Sut.EnvironmentFolder.AbsolutePath, "VagrantFile");
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
    internal class VagranFileTests : Test<VagrantFileTestsFixture>
    {
        [Test]
        public void Can_create_basic_vagrant_file()
        {
            //Arrange
            Fixture.Sut.SetLocation(new Uri(@"C:/IntegrationTest/Vagrant/"));
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            //Act
            Fixture.SaveVagrantFile();

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
            Fixture.Sut.SetLocation(new Uri(@"C:/IntegrationTest/Vagrant/"));
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.Network.ExposedPorts.Add(10, 8010);
            Fixture.Sut.Network.ExposedPorts.Add(32, 8032);

            //Act
            Fixture.SaveVagrantFile();

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
            Fixture.Sut.SetLocation(new Uri(@"C:/IntegrationTest/Vagrant/"));
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.Provision.Shell.AddInlineScript("sudo apt-get update");
            Fixture.Sut.Provision.Shell.AddInlineScript("sudo apt-get install nano");

            //Act
            Fixture.SaveVagrantFile();

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
            Fixture.Sut.SetLocation(new Uri(@"C:/IntegrationTest/Vagrant/"));
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.Provision.Shell.AddExternalScript(new Uri("https://example.com/provisioner.sh"));
            Fixture.Sut.Provision.Shell.AddExternalScript(new Uri(@"C:\VagrantScripts\Test.sh"));

            //Act
            Fixture.SaveVagrantFile();

            //Assert
            Fixture.ReadVagrantFileContent(@"C:/IntegrationTest/Vagrant/VagrantFile")
                .Should().BeEquivalentTo(
                    "Vagrant.configure(2) do |config|" + Environment.NewLine +
                    "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                    "config.vm.provision :shell, path: \"https://example.com/provisioner.sh\"" + Environment.NewLine +
                    "config.vm.provision :shell, path: \"C:/VagrantScripts/Test.sh\"" + Environment.NewLine +
                    "end");
        }

        [Test]
        public void Can_use_docker_compose_files()
        {
            //Arrange
            Fixture.Sut.SetLocation(new Uri(@"C:/IntegrationTest/Vagrant/"));
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.Provision.AddDockerComposeFile(
                new Uri(@"C:\VagrantScripts\Test1\docker-compose.yml"),
                new VagrantUri("./First/docker-compose.yml"),
                false);

            Fixture.Sut.Provision.AddDockerComposeFile(
                new Uri(@"C:\VagrantScripts\Test2\docker-compose.yml"),
                new VagrantUri("./Second/docker-compose.yml"),
                false);

            //Act
            Fixture.SaveVagrantFile();

            //Assert
            Fixture.ReadVagrantFileContent(@"C:/IntegrationTest/Vagrant/VagrantFile")
                .Should().BeEquivalentTo(
                    "Vagrant.configure(2) do |config|" + Environment.NewLine +
                    "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                    "config.vm.provision :file, source: \"C:/VagrantScripts/Test1/docker-compose.yml\", destination: \"./First/docker-compose.yml\"" + Environment.NewLine +
                    "config.vm.provision :file, source: \"C:/VagrantScripts/Test2/docker-compose.yml\", destination: \"./Second/docker-compose.yml\"" + Environment.NewLine +
                    "config.vm.provision :docker" + Environment.NewLine +
                    "config.vm.provision :shell, inline: <<-SHELL" + Environment.NewLine +
                    "sudo apt-get update" + Environment.NewLine +
                    "sudo apt-get install python-pip -y" + Environment.NewLine +
                    "sudo pip install docker-compose" + Environment.NewLine +
                    "cd First/ && sudo docker-compose up -d" + Environment.NewLine +
                    "cd Second/ && sudo docker-compose up -d" + Environment.NewLine +
                    "SHELL" + Environment.NewLine +
                    "end");
        }

        [Test]
        public void Can_use_files()
        {
            //Arrange
            Fixture.Sut.SetLocation(new Uri(@"C:/IntegrationTest/Vagrant/"));
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.Provision.Files.Add(new Uri(@"C:\Vagrant\File1"), new VagrantUri("./Copied/File1"));
            Fixture.Sut.Provision.Files.Add(new Uri(@"C:\Vagrant\File2"), new VagrantUri("./Copied/File2"));

            //Act
            Fixture.SaveVagrantFile();

            //Assert
            Fixture.ReadVagrantFileContent(@"C:/IntegrationTest/Vagrant/VagrantFile")
                .Should().BeEquivalentTo(
                    "Vagrant.configure(2) do |config|" + Environment.NewLine +
                    "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                    "config.vm.provision :file, source: \"C:/Vagrant/File1\", destination: \"./Copied/File1\"" + Environment.NewLine +
                    "config.vm.provision :file, source: \"C:/Vagrant/File2\", destination: \"./Copied/File2\"" + Environment.NewLine +
                    "end");
        }

        [Test]
        public void Can_use_virtual_box_provider()
        {
            //Arrange
            Fixture.Sut.SetLocation(new Uri(@"C:/IntegrationTest/Vagrant/"));
            Fixture.Sut.StartFromBox("hashicorp/precise64");

            Fixture.Sut.Provider = new VirtualBoxProviderDefinition
            {
                Cpus = 3,
                Memory = 4096,
                Customizations = Enumerable.Empty<VirtualBoxCustomization>(), //not implemented yet
                ShouldUseLinkedClones = true,
                VirtualMachineName = "test"
            };

            //Act
            Fixture.SaveVagrantFile();

            //Assert
            Fixture.ReadVagrantFileContent(@"C:/IntegrationTest/Vagrant/VagrantFile")
                .Should().BeEquivalentTo(
                    "Vagrant.configure(2) do |config|" + Environment.NewLine +
                    "config.vm.box = \"hashicorp/precise64\"" + Environment.NewLine +
                    "config.vm.provider \"virtualbox\" do |v|" + Environment.NewLine +
                    "v.gui = false" + Environment.NewLine +
                    "v.name = \"test\"" + Environment.NewLine +
                    "v.linked_clone = true" + Environment.NewLine +
                    "v.memory = 4096" + Environment.NewLine +
                    "v.cpus = 3" + Environment.NewLine +
                    "end" + Environment.NewLine +
                    "end");
        }
    }
}
