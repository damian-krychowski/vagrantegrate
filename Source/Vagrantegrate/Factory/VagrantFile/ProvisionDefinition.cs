using System;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class ProvisionDefinition
    {
        public ShellProvisionDefinitions Shell { get; } = new ShellProvisionDefinitions();
        public FileProvisionDefinitions Files { get; } = new FileProvisionDefinitions();
        public DockerProvisionDefitions Docker { get; } = new DockerProvisionDefitions();

        public void AddDockerComposeFile(string dockerComposeFilePath, LinuxUri destination)
        {
            CheckInput(dockerComposeFilePath, nameof(dockerComposeFilePath));

            Docker.Install();

            Files.Add(dockerComposeFilePath, destination.File);
            Shell.AddInlineScript("sudo apt-get update");
            Shell.AddInlineScript("sudo apt-get install docker-compose -y");

            Shell.AddInlineScript(string.IsNullOrEmpty(destination.Path)
                ? "sudo docker-compose up -d"
                : $"cd /{destination.Path} && sudo docker-compose up -d");
        }

        private static void CheckInput(string inputValue, string inputName)
        {
            if (String.IsNullOrEmpty(inputValue)) throw new ArgumentException("Argument is null or empty", inputName);
        }
    }
}