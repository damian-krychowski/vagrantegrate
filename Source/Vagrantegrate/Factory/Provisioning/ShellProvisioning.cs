using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning
{
    internal class ShellProvisioning : IShellProvisioning
    {
        private readonly VagrantFileDefinition _vagrantFile;

        public ShellProvisioning(VagrantFileDefinition vagrantFile)
        {
            _vagrantFile = vagrantFile;
        }

        public IShellProvisioning WithShellExternalScript(string scriptFilePath)
        {
            _vagrantFile.AddShellExternalScript(scriptFilePath);
            return this;
        }

        public IShellProvisioning WithShellInlineScript(string scriptBody)
        {
            _vagrantFile.AddShellInlineScript(scriptBody);
            return this;
        }

        public IShellProvisioning WithNodeJs()
        {
            this.WithShellInlineScript("sudo add-apt-repository ppa:chris-lea/node.js")
                .WithShellInlineScript("sudo apt-get update")
                .WithShellInlineScript("sudo apt-get install nodejs -y")
                .WithShellInlineScript("sudo npm install npm -g");

            return this;
        }
    }
}