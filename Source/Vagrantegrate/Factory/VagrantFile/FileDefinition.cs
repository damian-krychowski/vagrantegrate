using System.Collections.Generic;
using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class FileProvisionDefinitions : IVagrantFileBuilder
    {
        private readonly List<FileDefinition> _fileDefinitions = new List<FileDefinition>();

        public void Add(string sourcePath, string destinationPath)
        {
            _fileDefinitions.Add(new FileDefinition(sourcePath,destinationPath));    
        }

        public StringBuilder AppendToVagrantFile(StringBuilder vagrantFileBuilder)
        {
            foreach (var fileDefinition in _fileDefinitions)
            {
                vagrantFileBuilder.AppendLine(fileDefinition.ToString());
            }

            return vagrantFileBuilder;
        }
    }

    internal class FileDefinition
    {
        private readonly string _sourcePath;
        private readonly string _destinationPath;

        public FileDefinition(string sourcePath, string destinationPath)
        {
            _sourcePath = sourcePath;
            _destinationPath = destinationPath;
        }

        public override string ToString()
        {
            return $"config.vm.provision :file, source: \"{_sourcePath}\", destination: \"{_destinationPath}\"";
        }
    }
}