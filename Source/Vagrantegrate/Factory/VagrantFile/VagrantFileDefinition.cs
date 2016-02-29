using System;
using System.Collections.Generic;
using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class VagrantFileDefinition
    {
        private readonly IVagrantFileFactory _vagrantFileFactory;
        private string _fileLocation;
        private string _boxName;

        public ProvisionDefinition Provision { get; } = new ProvisionDefinition();
        public NetworkingDefinition Network { get; } = new NetworkingDefinition();

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


        private static void CheckInput(string inputValue, string inputName)
        {
            if (String.IsNullOrEmpty(inputValue)) throw new ArgumentException("Argument is null or empty", inputName);
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

            builder = Network.ExposedPorts.AppendToVagrantFile(builder);
            builder = Provision.Files.AppendToVagrantFile(builder);
            builder = Provision.Docker.AppendToVagrantFile(builder);
            builder = Provision.Shell.AppendToVagrantFile(builder);

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