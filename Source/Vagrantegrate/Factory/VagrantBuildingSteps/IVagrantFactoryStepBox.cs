using System;
using Vagrantegrate.Factory.Provisioning;

namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepBox
    {
        IVagrantFactoryStepFinalization WithVagrantfile(string vagrantfile);

        IVagrantFactoryStepProvider WithBox(string boxName);
        IVagrantFactoryStepProvider WithTrusty32();
        IVagrantFactoryStepProvider WithTrusty64();
        IVagrantFactoryStepProvider WithWily32();
        IVagrantFactoryStepProvider WithWily64();
        IVagrantFactoryStepProvider WithPrecise32();
        IVagrantFactoryStepProvider WithPrecise64();
    }
}