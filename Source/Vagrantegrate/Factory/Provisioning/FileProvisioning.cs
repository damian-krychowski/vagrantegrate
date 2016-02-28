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
            _vagrantFile.AddFile(sourcePath, destinationPath);
            return this;
        }
    }
}