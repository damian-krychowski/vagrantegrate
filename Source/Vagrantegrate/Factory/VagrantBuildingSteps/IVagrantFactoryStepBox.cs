using System;
using Vagrantegrate.Factory.Provisioning;

namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepBox
    {
        IVagrantFactoryStepFinalization WithVagrantfile(string vagrantfile);

        IVagrantFactoryStepProvisioning WithBox(string boxName);
        IVagrantFactoryStepProvisioning WithTrusty32();
        IVagrantFactoryStepProvisioning WithTrusty64();
        IVagrantFactoryStepProvisioning WithWily32();
        IVagrantFactoryStepProvisioning WithWily64();
        IVagrantFactoryStepProvisioning WithPrecise32();
        IVagrantFactoryStepProvisioning WithPrecise64();
    }
}