using System;
using System.Collections.Generic;
using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal interface IVagrantFileBuilder
    {
        StringBuilder AppendToVagrantFile(StringBuilder vagrantFileBuilder);
    }


    internal class VagrantFileDefinition
    {
        private readonly IVagrantFileFactory _vagrantFileFactory;
        private string _fileLocation;
        private string _boxName;
        private string _systemName;

        private readonly ExposedPortDefinitions _exposedPorts = new ExposedPortDefinitions(); 
        private readonly ShellProvisionDefinitions _shellProvisionDefinitions = new ShellProvisionDefinitions();
        private readonly FileProvisionDefinitions _fileProvisionDefinitions = new FileProvisionDefinitions();
        private readonly DockerProvisionDefitions _dockerProvisionDefitions = new DockerProvisionDefitions();

        public string EnvironmentPath => _fileLocation;

        public VagrantFileDefinition(IVagrantFileFactory vagrantFileFactory)
        {
            _vagrantFileFactory = vagrantFileFactory;
        }

        public void SetLocation(string path)
        {
            CheckInput(path, nameof(path));

            _fileLocation = path;
        }

        public void StartFromBox(string boxName)
        {
            CheckInput(boxName, nameof(boxName));
            _boxName = boxName;
        }

        public void InitWith(string systemName)
        {
            CheckInput(systemName, nameof(systemName));
            _systemName = systemName;
        }

        public void AddExposedPort(int guestPort, int hostPort)
        {
            _exposedPorts.Add(guestPort, hostPort);
        }

        public void AddShellExternalScript(string scriptFilePath)
        {
           CheckInput(scriptFilePath, nameof(scriptFilePath));

          _shellProvisionDefinitions.AddExternalScript(scriptFilePath);
        }

        public void AddShellInlineScript(string inlineScriptBody)
        {
           _shellProvisionDefinitions.AddInlineScript(inlineScriptBody);
        }

        public void AddFile(string sourcePath, string destinationPath)
        {
            CheckInput(sourcePath, nameof(sourcePath));
            CheckInput(destinationPath, nameof(destinationPath));

            _fileProvisionDefinitions.AddFile(sourcePath, destinationPath);
        }

        public void AddDockerComposeFile(string dockerComposeFilePath, LinuxUri destination)
        {
            CheckInput(dockerComposeFilePath, nameof(dockerComposeFilePath));

            InstallDocker();
            AddFile(dockerComposeFilePath, destination.File);
            AddShellInlineScript("sudo apt-get update");
            AddShellInlineScript("sudo apt-get install docker-compose -y");

            AddShellInlineScript(string.IsNullOrEmpty(destination.Path)
                ? "sudo docker-compose up -d"
                : $"cd /{destination.Path} && sudo docker-compose up -d");
        }

        private static void CheckInput(string inputValue, string inputName)
        {
            if (String.IsNullOrEmpty(inputValue)) throw new ArgumentException("Argument is null or empty", inputName);
        }

        public void InstallDocker()
        {
            _dockerProvisionDefitions.Install();
        }

        public void Save()
        {
            if(string.IsNullOrEmpty(_fileLocation)) throw new InvalidOperationException("VagrantFile location cannot be null or empty.");

            _vagrantFileFactory.Create(ToString(), _fileLocation, "VagrantFile");
        }
        

        public override string ToString()
        {
            var builder = new StringBuilder();

            AppendFileStart(builder);
            AppendBoxLineIfUsed(builder);

            builder = _exposedPorts.AppendToVagrantFile(builder);
            builder = _fileProvisionDefinitions.AppendToVagrantFile(builder);
            builder = _dockerProvisionDefitions.AppendToVagrantFile(builder);
            builder = _shellProvisionDefinitions.AppendToVagrantFile(builder);

            AppendFileEnd(builder);

            return builder.ToString();
        }




        private static void AppendFileEnd(StringBuilder builder)
        {
            builder.Append("end");
        }

        private static void AppendFileStart(StringBuilder builder)
        {
            builder.AppendLine("Vagrant.configure(2) do |config|");
        }


        private void AppendBoxLineIfUsed(StringBuilder builder)
        {
            if (WasBoxUsed)
            {
                builder.AppendLine("config.vm.box = \"" + _boxName + "\"");
            }
        }
        
        private bool WasBoxUsed => _boxName != null;
    }
}