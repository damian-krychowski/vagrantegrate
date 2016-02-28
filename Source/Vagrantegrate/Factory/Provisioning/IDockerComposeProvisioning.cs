namespace Vagrantegrate.Factory.Provisioning
{
    public interface IDockerComposeProvisioning
    {
        IDockerComposeProvisioning WithDockerComposeFile(string dockerComposePath, string destinationPath);
    }
}