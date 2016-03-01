using System;

namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepFolder
    {
        IVagrantFactoryStepBox WithEnvironmentFolder(string environmentFolderPath);
    }
}