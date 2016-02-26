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
            if (Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath).Empty();
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            Directory.CreateDirectory(filePath);

            using (var fs = File.Create(filePath + "\\" + fileName))
            {
                var info = new UTF8Encoding(true).GetBytes(fileContent);
                fs.Write(info, 0, info.Length);
            }
        }
    }

    internal static class DirectoryExtensions
    {
        public static void Empty(this System.IO.DirectoryInfo directory)
        {
            foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
    }

}
