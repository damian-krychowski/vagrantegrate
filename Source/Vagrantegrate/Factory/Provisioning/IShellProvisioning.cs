using System;
using Vagrantegrate.Factory.VagrantFile;
using Vagrantegrate.Scripts;

namespace Vagrantegrate.Factory.Provisioning
{
    public interface IShellProvisioning
    {
        IShellProvisioning WithExternalScript(string scriptFilePath);
        IShellProvisioning WithInlineScript(string scriptBody);
    }

    public static class ShellProvisioningDefinitionsExtensions
    {
        public static IShellProvisioning WithNodeJs(this IShellProvisioning provisioning)
        {
            return provisioning
                .WithInlineScript(Linux.AptGet.AddRepository("ppa:chris-lea/node.js").ToString())
                .WithInlineScript(Linux.AptGet.Update.ToString())
                .WithInlineScript(Linux.AptGet.Install.NodeJs.ToString())
                .WithInlineScript(Linux.Npm.Install.Npm.ToString());
        }

        public static IShellProvisioning WithMongoDb(this IShellProvisioning provisioning)
        {
            return provisioning
                .WithInlineScript(Linux.AptGet.Update.ToString())
                .WithInlineScript(Linux.AptGet.Install.MongoDb.ToString());
        }

        public static IShellProvisioning WithEnv(this IShellProvisioning provisioning,
            string name, string value)
        {
            return provisioning.WithInlineScript(Linux.Export(name, value).ToString());
        }
    }
}