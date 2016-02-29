using System;

namespace Vagrantegrate.Factory.Provisioning
{
    public interface IDockerComposeProvisioning
    {
        IDockerComposeProvisioning WithDockerComposeFile(string dockerComposeFilePath, string destinationPath);
    }
}