using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class DockerComposeProvisionDefinitions : IVagrantFileBuilder
    {
        private readonly List<string> _dockerComposeFiles = new List<string>();
        private bool _shouldRebuildOnVagrantUp = false;

        public void AddDockerComposeFile(string dockerComposePath)
        {
            _dockerComposeFiles.Add(dockerComposePath);
        }

        public void ShouldRebuildOnVagrantUp()
        {
            _shouldRebuildOnVagrantUp = true;
        }

        public StringBuilder AppendToVagrantFile(StringBuilder vagrantFileBuilder)
        {
            if (WasDockerComposeUsed)
            {
                var builder = InstallVagrantDockerComposePlugin(vagrantFileBuilder);

                builder.AppendLine("config.vm.provision :docker");
                builder.AppendLine($"config.vm.provision :docker_compose, yml: [{DockerComposeFiles}], rebuild: {_shouldRebuildOnVagrantUp.ToString().ToLower()}, run: \"always\"");

                return builder;
            }

            return vagrantFileBuilder;
        }

        private string DockerComposeFiles=> string.Join(",", _dockerComposeFiles.Select(file => "\"" + file + "\""));

        private static StringBuilder InstallVagrantDockerComposePlugin(StringBuilder vagrantFileBuilder)
        {
            var builder = new StringBuilder();

            builder.AppendLine("unless Vagrant.has_plugin ? (\"vagrant-docker-compose\")");
            builder.AppendLine("system(\"vagrant plugin install vagrant-docker-compose\")");
            builder.AppendLine("puts \"Dependencies installed, please try the command again.\"");
            builder.AppendLine("exit");
            builder.AppendLine("end");

            builder.Append(vagrantFileBuilder);

            return builder;
        }

        private bool WasDockerComposeUsed => _dockerComposeFiles.Any();
    }
}