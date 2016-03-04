using System;
using System.Linq;
using Vagrantegrate.CommandLine;
using Vagrantegrate.Factory.Assumptions;
using Vagrantegrate.Factory.Networking;
using Vagrantegrate.Factory.Provisioner.VirtualBox;
using Vagrantegrate.Factory.Provisioning;
using Vagrantegrate.Factory.VagrantFile;
using Vagrantegrate.Factory.VagrantFile.Providers;

namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    internal class VagrantStepFactory :
        IVagrantFactoryStepFolder,
        IVagrantFactoryStepBox,
        IVagrantFactoryStepProvider,
        IVagrantFactoryStepProvisioning,
        IVagrantFactoryStepNetworking,
        IVagrantFactoryStepFinalization
    {
        private readonly ICmdExecutor _cmdExecutor;
        private readonly INetworkingFactory _networkingFactory;
        private readonly IVagrantFileFactory _vagrantFileFactory;
        private readonly IDefinitionAssumptions _definitionAssumptions;
        private readonly VagrantFileDefinition _vagrantFile;

        public VagrantStepFactory(
            ICmdExecutor cmdExecutor,
            INetworkingFactory networkingFactory,
            IVagrantFileFactory vagrantFileFactory,
            IDefinitionAssumptions definitionAssumptions)
        {
            _cmdExecutor = cmdExecutor;
            _networkingFactory = networkingFactory;
            _vagrantFileFactory = vagrantFileFactory;
            _definitionAssumptions = definitionAssumptions;

            _vagrantFile = new VagrantFileDefinition();
        }

        public IVagrantFactoryStepBox WithEnvironmentFolder(string environmentFolderPath)
        {
            _definitionAssumptions.AssumeDirectoryExists(environmentFolderPath);
            _vagrantFile.SetLocation(new Uri(environmentFolderPath));
            return this;
        }

        public IVagrantFactoryStepFinalization WithVagrantfile(string vagrantfile)
        {
            _definitionAssumptions.AssumeFileExists(vagrantfile);
            throw new NotImplementedException();
        }

        public IVagrantFactoryStepProvider WithBox(string boxName)
        {
            _vagrantFile.StartFromBox(boxName);
            return this;
        }

        public IVagrantFactoryStepProvider WithTrusty32()
        {
            return WithBox("ubuntu/trusty32");
        }

        public IVagrantFactoryStepProvider WithTrusty64()
        {
            return WithBox("ubuntu/trusty62");
        }

        public IVagrantFactoryStepProvider WithWily32()
        {
            return WithBox("ubuntu/wily32");
        }

        public IVagrantFactoryStepProvider WithWily64()
        {
            return WithBox("ubuntu/wily64");
        }

        public IVagrantFactoryStepProvider WithPrecise32()
        {
            return WithBox("ubuntu/precise32");
        }

        public IVagrantFactoryStepProvider WithPrecise64()
        {
            return WithBox("ubuntu/precise64");
        }

        public IVagrantFactoryStepProvisioning UseDefaultProvider()
        {
            return this;
        }

        public IVagrantFactoryStepProvisioning UseVirtualBox(Action<IVirtualBoxProvider> provider)
        {
            var vmProvider = new VirtualBoxProvider();
            provider(vmProvider);

            _vagrantFile.Provider = new VirtualBoxProviderDefinition
            {
                Cpus = vmProvider.Cpus,
                Customizations = vmProvider.Customizations.Select(c=> new VirtualBoxCustomization(c)),
                Memory = vmProvider.Memory,
                ShouldShowGui = vmProvider.ShouldShowGui,
                ShouldUseLinkedClones = vmProvider.ShouldUseLinkedClones,
                VirtualMachineName = vmProvider.VirtualMachineName
            };

            return this;
        }

        public IVagrantFactoryStepNetworking WithProvision(Action<IProvisioning> provisioning)
        {
            var provision = new Provisioning.Provisioning(_vagrantFile, _definitionAssumptions);
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

        public IVagrant CheckAndPrepare()
        {
            _definitionAssumptions.CheckAssumptions();
            return Prepare();
        }

        public IVagrant Prepare()
        {
            _vagrantFileFactory.Create(
                _vagrantFile.ToString(), 
                _vagrantFile.EnvironmentFolder.AbsolutePath, 
                "VagrantFile");

            return new Vagrant(_cmdExecutor, _vagrantFile.EnvironmentFolder);
        }

    }
}