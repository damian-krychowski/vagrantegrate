using System.Collections.Generic;
using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class ExposedPortDefinitions : IVagrantFileBuilder
    {
        private readonly List<ExposedPortDefinition> _portDefinitions = new List<ExposedPortDefinition>();

        public void Add(int guestPort, int hostPort)
        {
            _portDefinitions.Add(new ExposedPortDefinition(guestPort,hostPort));
        }

        public StringBuilder AppendToVagrantFile(StringBuilder vagrantFileBuilder)
        {
            foreach (var exposedPortDefinition in _portDefinitions)
            {
                vagrantFileBuilder.AppendLine(exposedPortDefinition.ToString());
            }

            return vagrantFileBuilder;
        }
    }


    internal class ExposedPortDefinition
    {
        private readonly int _guestPort;
        private readonly int _hostPort;

        public ExposedPortDefinition(int guestPort, int hostPort)
        {
            _guestPort = guestPort;
            _hostPort = hostPort;
        }

        public override string ToString()
        {
            return $"config.vm.network :forwarded_port, guest: {_guestPort}, host: {_hostPort}";
        }
    }
}