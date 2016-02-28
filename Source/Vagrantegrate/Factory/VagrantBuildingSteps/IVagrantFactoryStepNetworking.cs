using System;
using Vagrantegrate.Factory.Networking;

namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepNetworking
    {
        IVagrantFactoryStepFinalization WithNetworking(Action<INetworking> networking);
        IVagrantFactoryStepFinalization NoNetworking();
    }
}