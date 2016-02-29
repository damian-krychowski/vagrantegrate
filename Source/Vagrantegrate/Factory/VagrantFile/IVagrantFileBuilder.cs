using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal interface IVagrantFileBuilder
    {
        StringBuilder AppendToVagrantFile(StringBuilder vagrantFileBuilder);
    }
}