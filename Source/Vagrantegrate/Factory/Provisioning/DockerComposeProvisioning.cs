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

        public IDockerComposeProvisioning WithDockerComposeFile(string dockerComposeFilePath, string destinationPath)
        {
            _vagrantFile.Provision.AddDockerComposeFile(new Uri(dockerComposeFilePath), new VagrantUri(destinationPath)); 
            return this;
        }
    }
}