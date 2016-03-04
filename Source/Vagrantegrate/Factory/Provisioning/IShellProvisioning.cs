using System;
using Vagrantegrate.Factory.VagrantFile;
using Vagrantegrate.Scripts;

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
                .WithShellInlineScript(Linux.AptGet.AddRepository("ppa:chris-lea/node.js").ToString())
                .WithShellInlineScript(Linux.AptGet.Update.ToString())
                .WithShellInlineScript(Linux.AptGet.Install.NodeJs.ToString())
                .WithShellInlineScript(Linux.Npm.Install.Npm.ToString());
        }

        public static IShellProvisioning WithMongoDb(this IShellProvisioning provisioning)
        {
            return provisioning
                .WithShellInlineScript(Linux.AptGet.Update.ToString())
                .WithShellInlineScript(Linux.AptGet.Install.MongoDb.ToString());
        }

        public static IShellProvisioning WithEnv(this IShellProvisioning provisioning,
            string name, string value)
        {
            return provisioning.WithShellInlineScript(Linux.Export(name, value).ToString());
        }
    }
}