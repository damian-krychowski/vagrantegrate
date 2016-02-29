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
            _vagrantFile.Provision.Shell.AddExternalScript(scriptFilePath);
            return this;
        }

        public IShellProvisioning WithShellInlineScript(string scriptBody)
        {
            _vagrantFile.Provision.Shell.AddInlineScript(scriptBody);
            return this;
        }

        public IShellProvisioning WithNodeJs()
        {
            _vagrantFile.Provision.Shell.WithNodeJs();

            return this;
        }
    }
}