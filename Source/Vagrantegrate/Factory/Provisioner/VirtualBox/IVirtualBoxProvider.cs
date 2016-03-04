namespace Vagrantegrate.Factory.Provisioner.VirtualBox
{
    public interface IVirtualBoxProvider
    {
        IVirtualBoxProvider ShowGui();
        IVirtualBoxProvider WithVmName(string name);
        IVirtualBoxProvider UseLinkedClones();
        IVirtualBoxProvider WithCustomization(params string[] commandParts);
        IVirtualBoxProvider WithMemory(int ram);
        IVirtualBoxProvider WithCpus(int cpus);
    }
}