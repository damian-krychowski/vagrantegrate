using System;
using Vagrantegrate.Factory.Assumptions;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning
{
    internal class ShellProvisioning : IShellProvisioning
    {
        private readonly VagrantFileDefinition _vagrantFile;
        private readonly IDefinitionAssumptions _definitionAssumptions;

        public ShellProvisioning(
            VagrantFileDefinition vagrantFile,
            IDefinitionAssumptions definitionAssumptions)
        {
            _vagrantFile = vagrantFile;
            _definitionAssumptions = definitionAssumptions;
        }

        public IShellProvisioning WithExternalScript(string scriptFilePath)
        {
            _definitionAssumptions.AssumeFileExists(scriptFilePath);

            _vagrantFile.Provision.Shell.AddExternalScript(
                new Uri(scriptFilePath));

            return this;
        }

        public IShellProvisioning WithInlineScript(string scriptBody)
        {
            _vagrantFile.Provision.Shell.AddInlineScript(scriptBody);
            return this;
        }
    }
}