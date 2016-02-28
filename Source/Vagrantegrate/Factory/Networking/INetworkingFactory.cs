using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Networking
{
    internal interface INetworkingFactory
    {
        INetworking Create(VagrantFileDefinition vagrantFile);
    }

    internal class NetworkingFactory : INetworkingFactory
    {
        public INetworking Create(VagrantFileDefinition vagrantFile)
        {
            return new Networking(vagrantFile);
        }
    }
}   