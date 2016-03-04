using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal abstract class ProviderDefinition : IVagrantFileBuilder
    {
        public StringBuilder AppendToVagrantFile(StringBuilder vagrantFileBuilder)
        {
            AppendProviderStart(vagrantFileBuilder);
            AppendProviderBody(vagrantFileBuilder);
            AppendProviderEnd(vagrantFileBuilder);

            return vagrantFileBuilder;
        }

        private void AppendProviderStart(StringBuilder vagrantFileBuilder)
        {
            vagrantFileBuilder.AppendLine($"config.vm.provider \"{ProviderName}\" do |v|");
        }

        private void AppendProviderEnd(StringBuilder vagrantFileBuilder)
        {
            vagrantFileBuilder.AppendLine("end");
        }

        protected abstract string ProviderName { get; }
        protected abstract void AppendProviderBody(StringBuilder vagrantFileBuilder);
    }
}