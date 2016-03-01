namespace Vagrantegrate.Factory.Provisioning.DockerCompose
{
    internal class DockerComposeProvisioningBuilder :
        IDockerComposeProvisioningSource,
        IDockerComposeProvisioningDestination,
        IDockerComposeProvisioningFolder
    {
        public string DockerComposeFileSource { get; private set; }
        public string DockerComposeFileDestination { get; private set; }
        public bool ShouldIncludeFolder { get; private set; }

        public IDockerComposeProvisioningDestination From(string dockerComposeFilePath)
        {
            DockerComposeFileSource = dockerComposeFilePath;
            return this;
        }

        public IDockerComposeProvisioningFolder To(string destinationPath)
        {
            DockerComposeFileDestination = destinationPath;
            return this;
        }

        public void IncludeContainingFolder()
        {
            ShouldIncludeFolder = true;
        }
    }
}