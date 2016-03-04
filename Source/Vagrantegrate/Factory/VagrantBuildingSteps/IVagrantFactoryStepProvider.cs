using System;
using Vagrantegrate.Factory.Provisioner.VirtualBox;
using Vagrantegrate.Factory.Provisioning;

namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepProvider
    {
        IVagrantFactoryStepProvisioning UseDefaultProvider();
        IVagrantFactoryStepProvisioning UseVirtualBox(Action<IVirtualBoxProvider> provider);
    }
    
}