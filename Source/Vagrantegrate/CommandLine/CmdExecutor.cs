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
        ICmdExecutor WithWorkingDirectory(Uri path);
        void Execute(string command);
    }

    internal class CmdExecutor : ICmdExecutor
    {
        private Uri _workingDirectory;

        public ICmdExecutor WithWorkingDirectory(Uri path)
        {
            _workingDirectory = path;
            return this;
        }

        public void Execute(string command)
        {
            var processStartInfo = new ProcessStartInfo("cmd.exe")
            {
                WorkingDirectory = _workingDirectory.AbsolutePath,
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
