namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepFinalization
    {
        IVagrant CheckAndPrepare();
        IVagrant Prepare();
    }
}