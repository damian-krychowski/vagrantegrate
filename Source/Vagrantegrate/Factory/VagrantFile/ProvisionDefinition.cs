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
            Shell.AddInlineScript("sudo apt-get update");
            Shell.AddInlineScript("sudo apt-get install docker-compose -y");

            Shell.AddInlineScript(destination.IsFileLocatedInRoot()
                ? "sudo docker-compose up -d"
                : $"cd {destination.LocationPathRelativeToRoot()} && sudo docker-compose up -d");
        }

        private static void CheckInput(string inputValue, string inputName)
        {
            if (String.IsNullOrEmpty(inputValue)) throw new ArgumentException("Argument is null or empty", inputName);
        }
    }
}