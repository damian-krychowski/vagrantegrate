using System;
using Vagrantegrate.Factory.Assumptions;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning.DockerCompose
{
    internal class DockerComposeProvisioning : IDockerComposeProvisioning
    {
        private readonly VagrantFileDefinition _vagrantFile;
        private readonly IDefinitionAssumptions _definitionAssumptions;

        public DockerComposeProvisioning(
            VagrantFileDefinition vagrantFile,
            IDefinitionAssumptions definitionAssumptions)
        {
            _vagrantFile = vagrantFile;
            _definitionAssumptions = definitionAssumptions;
        }

        public IDockerComposeProvisioning WithDockerComposeFile(
            Action<IDockerComposeProvisioningSource> dockerComposeProvisioningBuilder)
        {
            var builder = new DockerComposeProvisioningBuilder();
            dockerComposeProvisioningBuilder(builder);

            _definitionAssumptions.AssumeFileExists(builder.DockerComposeFileSource);

            _vagrantFile.Provision.AddDockerComposeFile(
                new Uri(builder.DockerComposeFileSource),
                new VagrantUri(builder.DockerComposeFileDestination),
                builder.ShouldIncludeFolder);

            return this;
        }
    }
}