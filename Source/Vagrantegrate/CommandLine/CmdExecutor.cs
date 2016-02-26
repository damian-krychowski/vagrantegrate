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
        ICmdExecutor WithCommand(string command);
        void Execute();
    }

    internal class CmdExecutor : ICmdExecutor
    {
        private readonly List<string> _commands = new List<string>();  

        public ICmdExecutor WithCommand(string command)
        {
            _commands.Add(command);
            return this;
        }

        public void Execute()
        {
            var processStartInfo = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var process = Process.Start(processStartInfo);

         
            if (process != null)
            {
                foreach (var command in _commands)
                {
                    process.StandardInput.WriteLine(command);
                }

                process.StandardInput.Close();
                process.WaitForExit();
            }
        }
    }
}
