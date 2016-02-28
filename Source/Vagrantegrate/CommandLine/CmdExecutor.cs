using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vagrantegrate.CommandLine
{
    internal interface ICmdExecutor
    {
        ICmdExecutor WithWorkingDirectory(string path);
        void Execute(string command);
    }

    internal class CmdExecutor : ICmdExecutor
    {
        private string _workingDirectory;

        public ICmdExecutor WithWorkingDirectory(string path)
        {
            _workingDirectory = path;
            return this;
        }

        public void Execute(string command)
        {
            var processStartInfo = new ProcessStartInfo("cmd.exe")
            {
                WorkingDirectory = _workingDirectory,
                Arguments = "/c " + command,
                RedirectStandardOutput = false,
                UseShellExecute = true,
                CreateNoWindow = false
            };
            var cmd = Process.Start(processStartInfo);
            cmd.WaitForExit();
        }
    }
}
