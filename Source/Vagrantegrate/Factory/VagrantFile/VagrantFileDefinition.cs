using System;
using System.Collections.Generic;
using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class VirtualBoxCustomization : IVagrantFileBuilder
    {
        private readonly string[] _commandParts;

        public VirtualBoxCustomization(string[] commandParts)
        {
            _commandParts = commandParts;
        }

        public StringBuilder AppendToVagrantFile(StringBuilder vagrantFileBuilder)
        {
            throw new NotImplementedException();
        }
    }

    internal class VagrantFileDefinition
    {
        
        private string _boxName;

        public ProvisionDefinition Provision { get; } = new ProvisionDefinition();
        public NetworkingDefinition Network { get; } = new NetworkingDefinition();
        public ProviderDefinition Provider { get; set; }

        public Uri EnvironmentFolder { get; private set; }

        public void SetLocation(Uri environmentFolder)
        {
            EnvironmentFolder = environmentFolder;
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

        public override string ToString()
        {
            var builder = new StringBuilder();

            AppendFileStart(builder);
            AppendBoxLineIfUsed(builder);
            AppendProvider(builder);

            builder = Network.ExposedPorts.AppendToVagrantFile(builder);
            builder = Provision.Files.AppendToVagrantFile(builder);
            builder = Provision.Docker.AppendToVagrantFile(builder);
            builder = Provision.Shell.AppendToVagrantFile(builder);

            AppendFileEnd(builder);

            return builder.ToString();
        }

        private void AppendProvider(StringBuilder builder)
        {
            Provider?.AppendToVagrantFile(builder);
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