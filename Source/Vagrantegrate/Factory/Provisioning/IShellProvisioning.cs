namespace Vagrantegrate.Factory.Provisioning
{
    public interface IShellProvisioning
    {
        IShellProvisioning WithShellExternalScript(string scriptFilePath);
        IShellProvisioning WithShellInlineScript(string scriptBody);
        IShellProvisioning WithNodeJs();
    }
}