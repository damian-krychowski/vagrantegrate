using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class DockerProvisionDefitions : IVagrantFileBuilder
    {
        private bool _shouldInstall = false;

        public void Install()
        {
            _shouldInstall = true;
        }

        public StringBuilder AppendToVagrantFile(StringBuilder vagrantFileBuilder)
        {
            if (_shouldInstall)
            {
                vagrantFileBuilder.AppendLine("config.vm.provision :docker");
            }

            return vagrantFileBuilder;
        }
    }
}