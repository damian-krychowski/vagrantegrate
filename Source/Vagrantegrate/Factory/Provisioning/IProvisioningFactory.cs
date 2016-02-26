using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning
{
    internal interface IProvisioningFactory
    {
        IProvisioning Create(VagrantFileDefinition vagrantFile);
    }

    internal class ProvisioningFactory : IProvisioningFactory
    {
        public IProvisioning Create(VagrantFileDefinition vagrantFile)
        {
            return new Vagrantegrate.Provisioning(vagrantFile);
        }
    }
}
