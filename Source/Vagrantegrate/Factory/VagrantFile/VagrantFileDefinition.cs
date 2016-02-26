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
        private string _systemName;
        private readonly List<ExposedPortDefinition> _exposedPorts = new List<ExposedPortDefinition>(); 
        private readonly List<ShellExternalScriptDefinition> _externalShellScriptDefinitions = new List<ShellExternalScriptDefinition>(); 
        private readonly ShellInlineScriptDefinition _shellInlineScriptDefinition = new ShellInlineScriptDefinition();

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
            _exposedPorts.Add(new ExposedPortDefinition(guestPort, hostPort));
        }

        public void AddShellExternalScript(string scriptFilePath)
        {
            if (String.IsNullOrEmpty(scriptFilePath))
                throw new ArgumentException("Argument is null or empty", nameof(scriptFilePath));

            _externalShellScriptDefinitions.Add(new ShellExternalScriptDefinition(scriptFilePath));
        }

        public void AddShellInlineScript(string inlineScriptBody)
        {
            _shellInlineScriptDefinition.AddScript(inlineScriptBody);
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
            AppendExposedPorts(builder);
            AppendInlineShellScripts(builder);
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

        private void AppendInlineShellScripts(StringBuilder builder)
        {
            builder.Append(_shellInlineScriptDefinition.ToString());
        }

        private void AppendExposedPorts(StringBuilder builder)
        {
            foreach (var exposedPortDefinition in _exposedPorts)
            {
                builder.AppendLine(exposedPortDefinition.ToString());
            }
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