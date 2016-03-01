namespace Vagrantegrate.Factory.VagrantFile
{
    internal class ShellExternalScriptDefinition
    {
        private readonly string _scriptFilePath;

        public ShellExternalScriptDefinition(string scriptFilePath)
        {
            _scriptFilePath = scriptFilePath;
        }

        public override string ToString()
        {
            return $"config.vm.provision :shell, path: \"{_scriptFilePath}\"";
        }
    }
}