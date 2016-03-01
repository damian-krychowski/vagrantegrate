using System;
using System.Collections.Generic;
using System.Text;
using Vagrantegrate.Factory.Provisioning;
using Vagrantegrate.Linux;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class ShellProvisionDefinitions : IVagrantFileBuilder
    {
        private readonly ShellInlineScriptDefinition _shellInlineScriptDefinition =new ShellInlineScriptDefinition();
        private readonly List<ShellExternalScriptDefinition> _shellExternalScriptDefinitions = new List<ShellExternalScriptDefinition>();

        public ShellProvisionDefinitions AddInlineScript(string scriptBody)
        {
            _shellInlineScriptDefinition.AddScript(scriptBody);
            return this;
        }

        public ShellProvisionDefinitions AddInlineScript(LinuxScript script)
        {
            _shellInlineScriptDefinition.AddScript(script.ToString());
            return this;
        }

        public ShellProvisionDefinitions AddExternalScript(Uri scriptFile)
        {
            _shellExternalScriptDefinitions.Add(
                scriptFile.IsFile
                    ? new ShellExternalScriptDefinition(scriptFile.AbsolutePath)
                    : new ShellExternalScriptDefinition(scriptFile.AbsoluteUri));

            return this;
        }

        public StringBuilder AppendToVagrantFile(StringBuilder vagrantFileBuilder)
        {
            if (_shellInlineScriptDefinition.WasUsed)
            {
                vagrantFileBuilder.Append(_shellInlineScriptDefinition.ToString());
            }
          
            return vagrantFileBuilder;
        }
    }
}