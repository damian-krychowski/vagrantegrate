using System;
using Vagrantegrate.Scripts;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class ProvisionDefinition
    {
        public ShellProvisionDefinitions Shell { get; } = new ShellProvisionDefinitions();
        public FileProvisionDefinitions Files { get; } = new FileProvisionDefinitions();
        public DockerProvisionDefitions Docker { get; } = new DockerProvisionDefitions();

        public void AddDockerComposeFile(Uri dockerComposeFile, VagrantUri destination, bool includeFolder)
        {
            Docker.Install();

            if (includeFolder)
            {
                Files.Add(
                    dockerComposeFile.FolderUri(),
                    destination.FolderUri());
            }
            else
            {
                Files.Add(dockerComposeFile, destination);
            }
          
            Shell.AddInlineScript(Linux.AptGet.Update);
            Shell.AddInlineScript(Linux.AptGet.Install.DockerCompose);

            Shell.AddInlineScript(destination.IsFileLocatedInRoot()
                ? Linux.Docker.Compose.Up
                : Linux.Cd(destination.LocationPathRelativeToRoot()).And(Linux.Docker.Compose.Up));
        }

        private static void CheckInput(string inputValue, string inputName)
        {
            if (String.IsNullOrEmpty(inputValue)) throw new ArgumentException("Argument is null or empty", inputName);
        }
    }
}