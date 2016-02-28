using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning
{
    internal class DockerProvisioning : IDockerProvisioning
    {
        private readonly VagrantFileDefinition _vagrantFile;

        public DockerProvisioning(VagrantFileDefinition vagrantFile)
        {
            _vagrantFile = vagrantFile;
        }
    }
}