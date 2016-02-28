namespace Vagrantegrate.Factory.Provisioning
{
    public interface IFileProvisioning
    {
        IFileProvisioning WithFile(string sourcePath, string destinationPath);
    }
}