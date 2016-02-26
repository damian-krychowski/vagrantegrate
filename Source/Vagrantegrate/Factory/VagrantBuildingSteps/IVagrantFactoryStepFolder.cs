namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepFolder
    {
        IVagrantFactoryStepBox WithEnvironmentFolder(string path);
        IVagrantFactoryStepBox WithDefaultLocation();
    }
}