using System.Collections.Generic;
using System.Text;

namespace Vagrantegrate.Factory.VagrantFile.Providers
{
    internal class VirtualBoxProviderDefinition : ProviderDefinition
    {
        public bool ShouldShowGui { get; set; }
        public string VirtualMachineName { get; set; }
        public bool ShouldUseLinkedClones { get; set; }
        public IEnumerable<VirtualBoxCustomization> Customizations { get; set; }
        public int? Memory { get; set; }
        public int? Cpus { get; set; }
        
        protected override string ProviderName => "virtualbox";

        protected override void AppendProviderBody(StringBuilder vagrantFileBuilder)
        {
            AppendShowGui(vagrantFileBuilder);
            AppendMachineName(vagrantFileBuilder);
            AppendLinkedClone(vagrantFileBuilder);
            AppendCustomizations(vagrantFileBuilder);
            AppendMemory(vagrantFileBuilder);
            AppendCpus(vagrantFileBuilder);
        }

        private void AppendCpus(StringBuilder vagrantFileBuilder)
        {
            if (Cpus.HasValue)
            {
                vagrantFileBuilder.AppendLine($"v.cpus = {Cpus}");
            }
        }

        private void AppendMemory(StringBuilder vagrantFileBuilder)
        {
            if (Memory.HasValue)
            {
                vagrantFileBuilder.AppendLine($"v.memory = {Memory}");
            }
        }

        private void AppendCustomizations(StringBuilder vagrantFileBuilder)
        {
            foreach (var customization in Customizations)
            {
                customization.AppendToVagrantFile(vagrantFileBuilder);
            }
        }

        private void AppendLinkedClone(StringBuilder vagrantFileBuilder)
        {
            vagrantFileBuilder.AppendLowerCaseLine($"v.linked_clone = {ShouldUseLinkedClones}");
        }

        private void AppendMachineName(StringBuilder vagrantFileBuilder)
        {
            if (!string.IsNullOrEmpty(VirtualMachineName))
            {
                vagrantFileBuilder.AppendLine($"v.name = \"{VirtualMachineName}\"");
            }
        }

        private void AppendShowGui(StringBuilder vagrantFileBuilder)
        {
            vagrantFileBuilder.AppendLowerCaseLine($"v.gui = {ShouldShowGui}");
        }
    }
}