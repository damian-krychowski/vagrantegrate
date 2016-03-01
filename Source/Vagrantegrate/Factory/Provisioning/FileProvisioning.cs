using System;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning
{
    internal class FileProvisioning : IFileProvisioning
    {
        private readonly VagrantFileDefinition _vagrantFile;

        public FileProvisioning(VagrantFileDefinition vagrantFile)
        {
            _vagrantFile = vagrantFile;
        }

        public IFileProvisioning WithFile(string sourcePath, string destinationPath)
        {
            _vagrantFile.Provision.Files.Add(
                new Uri(sourcePath), 
                new VagrantUri(destinationPath));

            return this;
        }

        public IFileProvisioning WithFolder(string sourcePath, string destinationPath)
        {
            _vagrantFile.Provision.Files.Add(
                new Uri(sourcePath),
                new VagrantUri(destinationPath));

            return this;
        }
    }
}