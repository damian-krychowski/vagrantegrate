namespace Vagrantegrate.Factory.VagrantBuildingSteps
{
    public interface IVagrantFactoryStepBox
    {
        IVagrantFactoryStepProvisioning WithBox(string boxName);
        IVagrantFactoryStepProvisioning WithInit(string systemName);
    }
}