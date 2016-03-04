using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vagrantegrate.CommandLine;
using Vagrantegrate.Factory;
using Vagrantegrate.Factory.Assumptions;
using Vagrantegrate.Factory.Networking;
using Vagrantegrate.Factory.Provisioning;
using Vagrantegrate.Factory.VagrantBuildingSteps;

namespace Vagrantegrate
{
    public static class IntegrationTestEnvironment
    {
        public static IVagrantFactoryStepFolder Prepare()
        {
            var assumptions = new DefinitionAssumptions();

            assumptions.AssumeVagrantInstalled();

            return new VagrantStepFactory(
                new CmdExecutor(),
                new NetworkingFactory(),
                new VagrantFileFactory(), 
                assumptions);
        }
    }
}
