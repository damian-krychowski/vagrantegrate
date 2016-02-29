using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vagrantegrate.CommandLine;


namespace Vagrantegrate
{
    public interface IVagrant
    {
        void Up();
        void Destroy();
        void Reload(bool runProvisioners);
    }

    internal class Vagrant : IVagrant
    {
        private readonly ICmdExecutor _cmdExecutor;
        private readonly Uri _environmentFolder;

        public Vagrant(
            ICmdExecutor cmdExecutor,
            Uri environmentFolder)
        {
            _cmdExecutor = cmdExecutor;
            _environmentFolder = environmentFolder;
        }

        public void Up()
        {
            _cmdExecutor
                .WithWorkingDirectory(_environmentFolder)
                .Execute(VagrantUpCmd);
        }

        public void Destroy()
        {
            _cmdExecutor
                .WithWorkingDirectory(_environmentFolder)
                .Execute(VagrantDestroyCmd);
        }

        public void Reload(bool runProvisioners)
        {
            _cmdExecutor
                .WithWorkingDirectory(_environmentFolder)
                .Execute(VagrantReloadCmd(runProvisioners));
        }

        private string VagrantUpCmd => "vagrant up";
        private string VagrantDestroyCmd => "vagrant destroy -f";

        private string VagrantReloadCmd(bool runProvisioners)
        {
            return runProvisioners
                ? "vagrant reload --provision"
                : "vagrant reload";
        }
    }
}
