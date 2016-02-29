using System;
using Vagrantegrate.CommandLine;
using Vagrantegrate.Factory.Networking;
using Vagrantegrate.Factory.Provisioning;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    internal class VagrantStepFactory :
        IVagrantFactoryStepFolder,
        IVagrantFactoryStepBox,
        IVagrantFactoryStepProvisioning,
        IVagrantFactoryStepNetworking,
        IVagrantFactoryStepFinalization
    {
        private readonly ICmdExecutor _cmdExecutor;
        private readonly IProvisioningFactory _provisioningFactory;
        private readonly INetworkingFactory _networkingFactory;
        private readonly VagrantFileDefinition _vagrantFile;

        public VagrantStepFactory(
            ICmdExecutor cmdExecutor,
            IProvisioningFactory provisioningFactory,
            INetworkingFactory networkingFactory,
            IVagrantFileFactory vagrantFileFactory)
        {
            _cmdExecutor = cmdExecutor;
            _provisioningFactory = provisioningFactory;
            _networkingFactory = networkingFactory;

            _vagrantFile = new VagrantFileDefinition(vagrantFileFactory);
        }

        public IVagrantFactoryStepBox WithEnvironmentFolder(string path)
        {
            _vagrantFile.SetLocation(path);
            return this;
        }

        public IVagrantFactoryStepBox WithDefaultLocation()
        {
            return this;
        }

        public IVagrantFactoryStepFinalization WithVagrantfile(string vagrantfilePath)
        {
            throw new NotImplementedException();
        }

        public IVagrantFactoryStepProvisioning WithBox(string boxName)
        {
            _vagrantFile.StartFromBox(boxName);
            return this;
        }

        public IVagrantFactoryStepProvisioning WithTrusty32()
        {
            return WithBox("ubuntu/trusty32");
        }

        public IVagrantFactoryStepProvisioning WithTrusty64()
        {
            return WithBox("ubuntu/trusty62");
        }

        public IVagrantFactoryStepProvisioning WithWily32()
        {
            return WithBox("ubuntu/wily32");
        }

        public IVagrantFactoryStepProvisioning WithWily64()
        {
            return WithBox("ubuntu/wily64");
        }

        public IVagrantFactoryStepProvisioning WithPrecise32()
        {
            return WithBox("ubuntu/precise32");
        }

        public IVagrantFactoryStepProvisioning WithPrecise64()
        {
            return WithBox("ubuntu/precise64");
        }

        public IVagrantFactoryStepNetworking WithProvision(Action<IProvisioning> provisioning)
        {
            var provision = _provisioningFactory.Create(_vagrantFile);
            provisioning(provision);

            return this;
        }

        public IVagrantFactoryStepNetworking NoProvisioning()
        {
            return this;
        }

        public IVagrantFactoryStepFinalization WithNetworking(Action<INetworking> networking)
        {
            var network = _networkingFactory.Create(_vagrantFile);
            networking(network);

            return this;
        }

        public IVagrantFactoryStepFinalization NoNetworking()
        {
            return this;
        }

        public IVagrant Prepare()
        {
            _vagrantFile.Save();
          
            return new Vagrant(_cmdExecutor, _vagrantFile.EnvironmentPath);
        }
    }
}