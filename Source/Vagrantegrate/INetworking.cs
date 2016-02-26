using Vagrantegrate.Factory.VagrantBuildingSteps;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate
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
            _vagrantFile.AddExposedPort(guestPort, hostPort);
            return this;
        }
    }
}