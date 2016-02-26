namespace Vagrantegrate.Factory.VagrantFile
{
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
            return $"config.vm.network \"forwarded_port\", guest: {_guestPort}, host: {_hostPort}";
        }
    }
}