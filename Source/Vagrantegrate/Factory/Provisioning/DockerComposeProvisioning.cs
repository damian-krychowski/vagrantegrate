﻿using System;
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

        public IDockerComposeProvisioning WithDockerComposeFile(string dockerComposePath, string destinationPath)
        {
            _vagrantFile.AddDockerComposeFile(dockerComposePath, new LinuxUri(destinationPath));
            return this;
        }
    }
}