namespace Vagrantegrate.Factory.Provisioning.DockerCompose
{
    public interface IDockerComposeProvisioningDestination
    {
        IDockerComposeProvisioningFolder To(string destinationPath);
    }
}