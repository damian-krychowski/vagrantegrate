﻿using System;
using Vagrantegrate.Factory.Assumptions;
using Vagrantegrate.Factory.Provisioning.DockerCompose;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning
{
    internal class Provisioning : IProvisioning
    {
        private readonly IShellProvisioning _shellProvisioning;
        private readonly IDockerProvisioning _dockerProvisioning;
        private readonly IDockerComposeProvisioning _dockerComposeProvisioning;
        private readonly IFileProvisioning _fileProvisioning;
        
        public Provisioning(
            VagrantFileDefinition vagrantFile,
            IDefinitionAssumptions definitionAssumptions)
        {
            _shellProvisioning = new ShellProvisioning(vagrantFile, definitionAssumptions);
            _dockerProvisioning = new DockerProvisioning(vagrantFile);
            _dockerComposeProvisioning = new DockerComposeProvisioning(vagrantFile, definitionAssumptions);
            _fileProvisioning = new FileProvisioning(vagrantFile, definitionAssumptions);
           
        }

        public IProvisioning WithDockerProvisioning(Action<IDockerProvisioning> provisioning)
        {
            provisioning(_dockerProvisioning);
            return this;
        }

        public IProvisioning WithDockerComposeProvisioning(Action<IDockerComposeProvisioning> provisioning)
        {
            provisioning(_dockerComposeProvisioning);
            return this;
        }

        public IProvisioning WithFileProvisioning(Action<IFileProvisioning> provisioning)
        {
            provisioning(_fileProvisioning);
            return this;
        }

        public IProvisioning WithShellProvisioning(Action<IShellProvisioning> provisioning)
        {
            provisioning(_shellProvisioning);
            return this;
        }
    }
}