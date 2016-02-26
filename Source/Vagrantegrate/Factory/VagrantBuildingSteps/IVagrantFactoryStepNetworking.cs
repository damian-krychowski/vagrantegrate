using System;

namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepNetworking
    {
        IVagrantFactoryStepFinalization WithNetworking(Action<INetworking> networking);
        IVagrantFactoryStepFinalization NoNetworking();
    }
}