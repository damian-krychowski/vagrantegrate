using System;

namespace Vagrantegrate.Factory.Provisioning
{
    public interface IProvisioning
    {
        IProvisioning WithDockerProvisioning(Action<IDockerProvisioning> provisioning);
        IProvisioning WithDockerComposeProvisioning(Action<IDockerComposeProvisioning> provisioning);
        IProvisioning WithFileProvisioning(Action<IFileProvisioning> provisioning);
        IProvisioning WithShellProvisioning(Action<IShellProvisioning> provisioning);
    }
}