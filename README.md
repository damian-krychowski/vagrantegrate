# vagrantegrate
Prepare environment for your integration tests with external components easily

Vagrantegrate is a library allowing to prepare and start virtual machines hosted with Vagrant. It is advanced builder covering the most important Vagrantfile settings. The aim was to provide an easy way to reproduce an environment with code, so it can be started, used and cleaned up during tests session.

Nuget: https://www.nuget.org/packages/vagrantegrate

Build: <image src="https://ci.appveyor.com/api/projects/status/a1gyrvbkbprd39qt/branch/master?svg=true">

## Environment example

```c#
    public void PrepareEnvironment()
    {
        IVagrant vagrant = IntegrationTestEnvironment.Prepare()
          .WithEnvironmentFolder("C:/Vagrant/Orion")    
          .WithWily64() /                               
          .UseVirtualBox(virtualBox => virtualBox       
              .WithMemory(2048)
              .WithCpus(2)
              .WithVmName("VagrantForOrion"))
          .WithProvision(provision => provision
              .WithDockerComposeProvisioning(dockercompose => dockercompose                   
                  .WithDockerComposeFile(orion => orion
                      .From(@"C:/Vagrant/OrionDocker/docker-compose.yml")
                      .To("./Orion/docker-compose.yml")
                      .IncludeContainingFolder())))
          .WithNetworking(networking => networking
              .WithPortForwarded(1026, 1026))
          .CheckAndPrepare();
        
        vagrant.Up();
    }
```
The piece of code presents simple environment prepared to host an example linux component. There are several steps performed:
- location for vagrant machine is selected
- base-box is selected
- virtual machine settings (like available memory, cpus etc.) are chosen
- machine provision is defined
- machine networking is defined

## Provider
Currently only VirtualBox provider is supported. Vagrant provider allows to define resource and hardware related stuff. For example Virtual Box offers following settings:

```c#
    public interface IVirtualBoxProvider
    {
        IVirtualBoxProvider ShowGui();
        IVirtualBoxProvider WithVmName(string name);
        IVirtualBoxProvider UseLinkedClones();
        IVirtualBoxProvider WithCustomization(params string[] commandParts);
        IVirtualBoxProvider WithMemory(int ram);
        IVirtualBoxProvider WithCpus(int cpus);
    }
```
## Provision
Provisioning is responible for all actions which should be taken to prepare machine for the first use. There are several ways to provision the machine:
- inline bash scripts can be executed,
- file scripts (*.sh) can be executed,
- files from host to guest can be copied, 
- docker containers can be started,

Vagrant offers far more options, but they are not supported currently.

#### Shell

```c#
    public interface IShellProvisioning
    {
        IShellProvisioning WithExternalScript(string scriptFilePath);
        IShellProvisioning WithInlineScript(string scriptBody);
    }
```

#### Files

```c#
    public interface IFileProvisioning
    {
        IFileProvisioning WithFile(string sourcePath, string destinationPath);
        IFileProvisioning WithFolder(string sourcePath, string destinationPath);
    }
```

#### Docker-Compose

```c#
    public interface IDockerComposeProvisioning
    {
        IDockerComposeProvisioning WithDockerComposeFile(
            Action<IDockerComposeProvisioningSource> dockerComposeProvisioningBuilder);
    }
    
    public interface IDockerComposeProvisioningSource
    {
        IDockerComposeProvisioningDestination From(string dockerComposeFilePath);
    }
    
    public interface IDockerComposeProvisioningDestination
    {
        IDockerComposeProvisioningFolder To(string destinationPath);
    }
    
    public interface IDockerComposeProvisioningFolder
    {
        void IncludeContainingFolder();
    }
```

## Network

VagrantFile has section dedicated to network settings. Guest ports can be mapped to host ports, to allow convinient way of communication between host and guest machines.

```c#
    public interface INetworking
    {
        INetworking WithPortForwarded(int guestPort, int hostPort);
    }
```
