using System;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning
{
    internal class DockerComposeProvisioning : IDockerComposeProvisioning
    {
        private readonly VagrantFileDefinition _vagrantFile;

        public DockerComposeProvisioning(VagrantFileDefinition vagrantFile)
        {
            _vagrantFile = vagrantFile;
        }

        public IDockerComposeProvisioning WithDockerComposeFile(string dockerComposePath)
        {
            _vagrantFile.AddDockerComposeFile(dockerComposePath);
            return this;
        }

        public void RebuildOnVagrantUp()
        {
            _vagrantFile.RebuildDockerComposeOnVagrantUp();
        }
    }
}