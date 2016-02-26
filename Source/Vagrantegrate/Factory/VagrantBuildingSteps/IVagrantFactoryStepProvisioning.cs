using System;

namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepProvisioning
    {
        IVagrantFactoryStepNetworking WithProvision(Action<IProvisioning> provisioning);
        IVagrantFactoryStepNetworking NoProvisioning();
    }
}