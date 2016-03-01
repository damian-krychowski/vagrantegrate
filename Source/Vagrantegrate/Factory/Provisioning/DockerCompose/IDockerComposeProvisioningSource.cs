namespace Vagrantegrate.Factory.Provisioning.DockerCompose
{
    public interface IDockerComposeProvisioningSource
    {
        IDockerComposeProvisioningDestination From(string dockerComposeFilePath);
    }
}