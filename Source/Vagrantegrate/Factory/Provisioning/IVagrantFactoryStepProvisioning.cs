using System;
using Vagrantegrate.Factory.VagrantBuildingSteps;

namespace Vagrantegrate.Factory.Provisioning
{
    public interface IVagrantFactoryStepProvisioning
    {
        IVagrantFactoryStepNetworking WithProvision(Action<IProvisioning> provisioning);
        IVagrantFactoryStepNetworking NoProvisioning();
    }
}