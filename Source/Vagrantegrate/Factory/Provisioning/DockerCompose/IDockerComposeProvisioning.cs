using System;

namespace Vagrantegrate.Factory.Provisioning.DockerCompose
{
    public interface IDockerComposeProvisioning
    {
        IDockerComposeProvisioning WithDockerComposeFile(
            Action<IDockerComposeProvisioningSource> dockerComposeProvisioningBuilder);
    }
}