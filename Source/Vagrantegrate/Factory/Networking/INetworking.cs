using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Networking
{
    public interface INetworking
    {
        INetworking WithPortForwarded(int guestPort, int hostPort);
    }

    internal class Networking : INetworking
    {
        private readonly VagrantFileDefinition _vagrantFile;

        public Networking(VagrantFileDefinition vagrantFile)
        {
            _vagrantFile = vagrantFile;
        }

        public INetworking WithPortForwarded(int guestPort, int hostPort)
        {
            _vagrantFile.Network.ExposedPorts.Add(guestPort, hostPort);
            return this;
        }
    }
}