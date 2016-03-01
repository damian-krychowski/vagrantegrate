using System;

namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepFolder
    {
        IVagrantFactoryStepBox InstallVagrantInFolder(string environmentFolderPath);
    }
}