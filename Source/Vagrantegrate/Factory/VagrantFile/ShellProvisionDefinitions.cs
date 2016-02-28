using System.Collections.Generic;
using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class ShellProvisionDefinitions : IVagrantFileBuilder
    {
        private readonly ShellInlineScriptDefinition _shellInlineScriptDefinition =new ShellInlineScriptDefinition();
        private readonly List<ShellExternalScriptDefinition> _shellExternalScriptDefinitions = new List<ShellExternalScriptDefinition>();

        public void AddInlineScript(string scriptBody)
        {
            _shellInlineScriptDefinition.AddScript(scriptBody);
        }

        public void AddExternalScript(string scriptFilePath)
        {
            _shellExternalScriptDefinitions.Add(new ShellExternalScriptDefinition(scriptFilePath));
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