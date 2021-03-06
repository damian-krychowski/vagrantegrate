namespace Vagrantegrate.Scripts
{
    internal static class Linux
    {
        public static LinuxScript Cd(string path) => new LinuxScript($"cd {path}");
        public static LinuxScript Export(string name, string value)=> new LinuxScript($"export {name}={value}");

        internal static class AptGet
        {
            public static LinuxScript Update => new LinuxScript("sudo apt-get update");
            public static LinuxScript AddRepository(string repository) => new LinuxScript($"sudo add-apt-get {repository}");
            
            internal static class Install
            {
                public static LinuxScript Pip => new LinuxScript("sudo apt-get install python-pip -y");
                public static LinuxScript DockerCompose => new LinuxScript("sudo apt-get install docker-compose -y");
                public static LinuxScript NodeJs => new LinuxScript("sudo apt-get install nodejs -y");
                public static LinuxScript MongoDb => new LinuxScript("sudo apt-get install mongodb-org -y");
            }
        }

        internal static class Docker
        {
            internal static class Compose
            {
                public static LinuxScript Up => new LinuxScript("sudo docker-compose up -d");
            }
        }

        internal static class Npm
        {
            internal static class Install
            {
                public static LinuxScript Npm => new LinuxScript("sudo npm install npm -g");
            }
        }

        internal static class Pip
        {
            internal static class Install
            {
                public static LinuxScript DockerCompose => new LinuxScript("sudo pip install docker-compose");
            }
        }
    }
}