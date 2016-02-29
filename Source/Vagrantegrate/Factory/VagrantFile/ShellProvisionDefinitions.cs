using System.Collections.Generic;
using System.Text;

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

        public ShellProvisionDefinitions AddExternalScript(string scriptFilePath)
        {
            _shellExternalScriptDefinitions.Add(new ShellExternalScriptDefinition(scriptFilePath));
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

    internal static class ShellProvisionDefinitionsExtensions
    {
        public static void WithNodeJs(this ShellProvisionDefinitions definitions)
        {
            definitions
                .AddInlineScript("sudo add-apt-repository ppa:chris-lea/node.js")
                .AddInlineScript("sudo apt-get update")
                .AddInlineScript("sudo apt-get install nodejs -y")
                .AddInlineScript("sudo npm install npm -g");
        }
    }
}