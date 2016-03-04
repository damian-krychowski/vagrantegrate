using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Vagrantegrate.Factory.Assumptions
{
    internal interface IDefinitionAssumptions
    {
        void AssumeVagrantInstalled();
        void AssumeFileExists(string filePath);
        void AssumeDirectoryExists(string directoryPath);

        void CheckAssumptions();
    }

    internal interface IAssumption
    {
        void Check();
    }

    internal class FileExistsAssumption : IAssumption
    {
        private readonly string _filePath;

        public FileExistsAssumption(string filePath)
        {
            _filePath = filePath;
        }

        public void Check()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException($"File {_filePath} does not exist.");
            }
        }
    }

    internal class DirectoryExistsAssumption : IAssumption
    {
        private readonly string _directoryPath;

        public DirectoryExistsAssumption(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public void Check()
        {
            if (!Directory.Exists(_directoryPath))
            {
                throw new DirectoryNotFoundException($"Directory {_directoryPath} does not exist.");
            }
        }
    }

    internal class VagrantInstalledAssumption : IAssumption
    {

        public void Check()
        {
            var installedApps = InstalledApplications();

            if (!installedApps.Contains("Vagrant"))
            {
                throw new InvalidOperationException("Vagrant is not installed.");
            }
        }

        private static IEnumerable<string> InstalledApplications()
        {
            // search in: CurrentUser
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (var keyName in key.GetSubKeyNames())
            {
                yield return key
                    .OpenSubKey(keyName)
                    ?.GetValue("DisplayName") as string;
            }

            // search in: LocalMachine_32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (var keyName in key.GetSubKeyNames())
            {
                yield return key
                  .OpenSubKey(keyName)
                  ?.GetValue("DisplayName") as string;
            }

            // search in: LocalMachine_64
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (var keyName in key.GetSubKeyNames())
            {
                yield return key
                  .OpenSubKey(keyName)
                  ?.GetValue("DisplayName") as string;
            }
        }
    }

    internal class DefinitionAssumptions : IDefinitionAssumptions
    {
        private readonly List<IAssumption> _assumptions = new List<IAssumption>(); 

        public void AssumeVagrantInstalled()
        {
            Assume(new VagrantInstalledAssumption());
        }

        public void AssumeFileExists(string filePath)
        {
           Assume(new FileExistsAssumption(filePath));
        }

        public void AssumeDirectoryExists(string directoryPath)
        {
          Assume(new DirectoryExistsAssumption(directoryPath));
        }

        public void CheckAssumptions()
        {
            _assumptions.ForEach(assumption=> assumption.Check());
        }

        private void Assume(IAssumption assumption)
        {
            _assumptions.Add(assumption);
        }
    }
}
