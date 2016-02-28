using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Vagrantegrate
{
    internal class LinuxUri
    {
        private readonly List<string> _pathElements;
         
        public LinuxUri(string path)
        {
            _pathElements = path.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries).ToList();

            if(_pathElements[0] != ".") throw new InvalidOperationException("Wrong path start.");
        }

        public string File => string.Join("/", _pathElements);
        public string Path => string.Join("/", _pathElements.Skip(1).Take(_pathElements.Count - 2));
    }
}
