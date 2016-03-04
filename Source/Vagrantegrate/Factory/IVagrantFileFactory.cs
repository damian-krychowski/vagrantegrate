using System;
using System.IO;
using System.Text;

namespace Vagrantegrate.Factory
{
    internal interface IVagrantFileFactory
    {
        void Create(string fileContent, string filePath, string fileName);
    }

    internal class VagrantFileFactory : IVagrantFileFactory
    {
        public void Create(string fileContent, string filePath, string fileName)
        {
            Directory.CreateDirectory(filePath).Empty();

            using (var fs = File.Create(filePath + "\\" + fileName))
            {
                var info = new UTF8Encoding(true).GetBytes(fileContent);
                fs.Write(info, 0, info.Length);
            }
        }
    }

    internal static class DirectoryExtensions
    {
        public static void Empty(this DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (var subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
    }

}
