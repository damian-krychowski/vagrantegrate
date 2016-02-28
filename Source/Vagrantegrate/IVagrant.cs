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
        private readonly string _environmentPath;

        public Vagrant(
            ICmdExecutor cmdExecutor,
            string environmentPath)
        {
            _cmdExecutor = cmdExecutor;
            _environmentPath = environmentPath;
        }

        public void Up()
        {
            _cmdExecutor
                .WithWorkingDirectory(_environmentPath)
                .Execute(VagrantUpCmd);
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void Reload(bool runProvisioners)
        {
            throw new NotImplementedException();
        }

        private string VagrantUpCmd => "vagrant up";
    }
}
