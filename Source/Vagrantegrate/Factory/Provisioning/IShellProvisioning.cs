using System;
using Vagrantegrate.Factory.VagrantFile;

namespace Vagrantegrate.Factory.Provisioning
{
    public interface IShellProvisioning
    {
        IShellProvisioning WithShellExternalScript(string scriptFilePath);
        IShellProvisioning WithShellInlineScript(string scriptBody);
    }

    public static class ShellProvisioningDefinitionsExtensions
    {
        public static IShellProvisioning WithNodeJs(this IShellProvisioning provisioning)
        {
            return provisioning
                .WithShellInlineScript(Linux.Linux.AptGet.AddRepository("ppa:chris-lea/node.js").ToString())
                .WithShellInlineScript(Linux.Linux.AptGet.Update.ToString())
                .WithShellInlineScript(Linux.Linux.AptGet.Install.NodeJs.ToString())
                .WithShellInlineScript(Linux.Linux.Npm.Install.Npm.ToString());
        }

        public static IShellProvisioning WithMongoDb(this IShellProvisioning provisioning)
        {
            return provisioning
                .WithShellInlineScript(Linux.Linux.AptGet.Update.ToString())
                .WithShellInlineScript(Linux.Linux.AptGet.Install.MongoDb.ToString());
        }
    }
}