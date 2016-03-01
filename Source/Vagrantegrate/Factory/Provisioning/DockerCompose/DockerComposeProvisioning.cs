using System;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning.DockerCompose
{
    internal class DockerComposeProvisioning : IDockerComposeProvisioning
    {
        private readonly VagrantFileDefinition _vagrantFile;

        public DockerComposeProvisioning(VagrantFileDefinition vagrantFile)
        {
            _vagrantFile = vagrantFile;
        }

        public IDockerComposeProvisioning WithDockerComposeFile(
            Action<IDockerComposeProvisioningSource> dockerComposeProvisioningBuilder)
        {
            var builder = new DockerComposeProvisioningBuilder();
            dockerComposeProvisioningBuilder(builder);

            _vagrantFile.Provision.AddDockerComposeFile(
                new Uri(builder.DockerComposeFileSource),
                new VagrantUri(builder.DockerComposeFileDestination),
                builder.ShouldIncludeFolder);

            return this;
        }
    }
}