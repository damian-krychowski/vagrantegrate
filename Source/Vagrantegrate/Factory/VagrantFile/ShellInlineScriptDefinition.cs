using System;
using System.Collections.Generic;
using System.Text;

namespace Vagrantegrate.Factory.VagrantFile
{
    internal class ShellInlineScriptDefinition
    {
        private readonly List<string> _scripts = new List<string>();

        public void AddScript(string scriptBody)
        {
            if (String.IsNullOrEmpty(scriptBody))
                throw new ArgumentException("Argument is null or empty", nameof(scriptBody));

            _scripts.Add(scriptBody);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            AppendCommandsStart(builder);
            AppendCommands(builder);
            AppendCommandsEnd(builder);

            return builder.ToString();
        }

        private static void AppendCommandsEnd(StringBuilder builder)
        {
            builder.AppendLine("SHELL");
        }

        private void AppendCommands(StringBuilder builder)
        {
            foreach (var script in _scripts)
            {
                builder.AppendLine(script);
            }
        }

        private static void AppendCommandsStart(StringBuilder builder)
        {
            builder.AppendLine("config.vm.provision \"shell\", inline: <<-SHELL");
        }
    }
}