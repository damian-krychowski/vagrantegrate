using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vagrantegrate.Factory.Provisioner.VirtualBox
{
    internal class VirtualBoxProvider: IVirtualBoxProvider
    {
        public bool ShouldShowGui { get; private set; }
        public string VirtualMachineName { get; private set; }
        public bool ShouldUseLinkedClones { get; private set; }
        public List<string[]> Customizations { get; } = new List<string[]>();
        public int Memory { get; private set; } = 1024;
        public int Cpus { get; private set; } = 1;

        public IVirtualBoxProvider ShowGui()
        {
            ShouldShowGui = true;
            return this;
        }

        public IVirtualBoxProvider WithVmName(string name)
        {
            VirtualMachineName = name;
            return this;
        }

        public IVirtualBoxProvider UseLinkedClones()
        {
            ShouldUseLinkedClones = true;
            return this;
        }

        public IVirtualBoxProvider WithCustomization(params string[] commandParts)
        {
            Customizations.Add(commandParts);
            return this;
        }

        public IVirtualBoxProvider WithMemory(int ram)
        {
            Memory = ram;
            return this;
        }

        public IVirtualBoxProvider WithCpus(int cpus)
        {
            Cpus = cpus;
            return this;
        }
    }
}
