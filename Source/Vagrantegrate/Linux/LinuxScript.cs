namespace Vagrantegrate.Factory.VagrantFile
{
    internal class LinuxScript
    {
        private readonly string _command;

        public LinuxScript(string command)
        {
            _command = command;
        }

        public LinuxScript And(LinuxScript script)
        {
            return new LinuxScript( _command + " && " +script.ToString());
        }

        public override string ToString()
        {
            return _command;
        }
    }
}