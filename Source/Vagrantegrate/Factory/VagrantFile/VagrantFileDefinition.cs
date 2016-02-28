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
        private readonly DockerComposeProvisionDefinitions _dockerComposeProvisionDefinitions = new DockerComposeProvisionDefinitions();

        public string EnvironmentPath => _fileLocation;

        public VagrantFileDefinition(IVagrantFileFactory vagrantFileFactory)
        {
            _vagrantFileFactory = vagrantFileFactory;
        }

        public void SetLocation(string path)
        {
            if (String.IsNullOrEmpty(path)) throw new ArgumentException("Argument is null or empty", nameof(path));

            _fileLocation = path;
        }

        public void StartFromBox(string boxName)
        {
            if (String.IsNullOrEmpty(boxName))
                throw new ArgumentException("Argument is null or empty", nameof(boxName));

            _boxName = boxName;
        }

        public void InitWith(string systemName)
        {
            if (String.IsNullOrEmpty(systemName))
                throw new ArgumentException("Argument is null or empty", nameof(systemName));

            _systemName = systemName;
        }

        public void AddExposedPort(int guestPort, int hostPort)
        {
            _exposedPorts.Add(guestPort, hostPort);
        }

        public void AddShellExternalScript(string scriptFilePath)
        {
            if (String.IsNullOrEmpty(scriptFilePath))
                throw new ArgumentException("Argument is null or empty", nameof(scriptFilePath));

          _shellProvisionDefinitions.AddExternalScript(scriptFilePath);
        }

        public void AddShellInlineScript(string inlineScriptBody)
        {
           _shellProvisionDefinitions.AddInlineScript(inlineScriptBody);
        }

        public void AddFile(string sourcePath, string destinationPath)
        {
            if (String.IsNullOrEmpty(sourcePath))
                throw new ArgumentException("Argument is null or empty", nameof(sourcePath));

            if (String.IsNullOrEmpty(destinationPath))
                throw new ArgumentException("Argument is null or empty", nameof(destinationPath));

            _fileProvisionDefinitions.AddFile(sourcePath, destinationPath);
        }

        public void AddDockerComposeFile(string dockerComposeFilePath)
        {
            if (String.IsNullOrEmpty(dockerComposeFilePath))
                throw new ArgumentException("Argument is null or empty", nameof(dockerComposeFilePath));

            _dockerComposeProvisionDefinitions.AddDockerComposeFile(dockerComposeFilePath);
        }

        public void RebuildDockerComposeOnVagrantUp()
        {
            _dockerComposeProvisionDefinitions.ShouldRebuildOnVagrantUp();
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
            builder = _dockerComposeProvisionDefinitions.AppendToVagrantFile(builder);
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