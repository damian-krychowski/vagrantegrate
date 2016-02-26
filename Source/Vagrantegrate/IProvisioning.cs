using Vagrantegrate.Factory.VagrantBuildingSteps;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate
{
    public interface IProvisioning
    {
        IProvisioning WithShellExternalScript(string scriptFilePath);
        IProvisioning WithShellInlineScript(string scriptBody);
    }

    internal class Provisioning : IProvisioning
    {
        private readonly VagrantFileDefinition _vagrantFile;

        public Provisioning(VagrantFileDefinition vagrantFile)
        {
            _vagrantFile = vagrantFile;
        }

        public IProvisioning WithShellExternalScript(string scriptFilePath)
        {
            _vagrantFile.AddShellExternalScript(scriptFilePath);
            return this;
        }

        public IProvisioning WithShellInlineScript(string scriptBody)
        {
            _vagrantFile.AddShellInlineScript(scriptBody);
            return this;
        }
    }
}