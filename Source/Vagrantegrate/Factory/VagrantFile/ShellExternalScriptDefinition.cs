namespace Vagrantegrate.Factory.VagrantFile
{
    internal class ShellExternalScriptDefinition
    {
        private readonly string _scriptFilePath;

        public ShellExternalScriptDefinition(string scriptFilePath)
        {
            _scriptFilePath = scriptFilePath;
        }
    }
}