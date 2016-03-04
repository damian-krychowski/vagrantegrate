using System;
using Vagrantegrate.Factory.Assumptions;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning
{
    internal class FileProvisioning : IFileProvisioning
    {
        private readonly VagrantFileDefinition _vagrantFile;
        private readonly IDefinitionAssumptions _definitionAssumptions;

        public FileProvisioning(
            VagrantFileDefinition vagrantFile,
            IDefinitionAssumptions definitionAssumptions)
        {
            _vagrantFile = vagrantFile;
            _definitionAssumptions = definitionAssumptions;
        }

        public IFileProvisioning WithFile(string sourcePath, string destinationPath)
        {
            _definitionAssumptions.AssumeFileExists(sourcePath);
            Add(sourcePath, destinationPath);

            return this;
        }

        public IFileProvisioning WithFolder(string sourcePath, string destinationPath)
        {
            _definitionAssumptions.AssumeDirectoryExists(sourcePath);
            Add(sourcePath, destinationPath);

            return this;
        }

        private void Add(string sourcePath, string destinationPath)
        {
            _vagrantFile.Provision.Files.Add(
                new Uri(sourcePath),
                new VagrantUri(destinationPath));
        }
    }
}