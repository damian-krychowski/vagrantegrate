using System;
using System.Collections.Generic;
using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class FileProvisionDefinitions : IVagrantFileBuilder
    {
        private readonly List<FileDefinition> _fileDefinitions = new List<FileDefinition>();

        public void Add(Uri source, VagrantUri destination)
        {
            _fileDefinitions.Add(new FileDefinition(
                source.AbsolutePath, 
                destination.AbsolutePath));
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