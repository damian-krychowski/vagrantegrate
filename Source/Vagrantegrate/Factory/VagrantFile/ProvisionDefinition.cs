using System;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class ProvisionDefinition
    {
        public ShellProvisionDefinitions Shell { get; } = new ShellProvisionDefinitions();
        public FileProvisionDefinitions Files { get; } = new FileProvisionDefinitions();
        public DockerProvisionDefitions Docker { get; } = new DockerProvisionDefitions();

        public void AddDockerComposeFile(Uri dockerComposeFile, VagrantUri destination)
        {
            Docker.Install();

            Files.Add(dockerComposeFile, destination);
            Shell.AddInlineScript(Linux.Linux.AptGet.Update);
            Shell.AddInlineScript(Linux.Linux.AptGet.Install.DockerCompose);

            Shell.AddInlineScript(destination.IsFileLocatedInRoot()
                ? Linux.Linux.Docker.Compose.Up
                : Linux.Linux.Cd(destination.LocationPathRelativeToRoot()).And(Linux.Linux.Docker.Compose.Up));
        }

        private static void CheckInput(string inputValue, string inputName)
        {
            if (String.IsNullOrEmpty(inputValue)) throw new ArgumentException("Argument is null or empty", inputName);
        }
    }
}