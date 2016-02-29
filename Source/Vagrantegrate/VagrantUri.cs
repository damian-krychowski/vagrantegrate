using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vagrantegrate
{
    public class VagrantUri
    {
       internal Uri Uri { get; }

        public VagrantUri(string path) : this(new Uri("vagrant:" + path))
        {
            
        }

        public VagrantUri(Uri uri)
        {
            if (!uri.IsVagrant())
            {
                throw new ArgumentException("Only vagrant uri is accepted.");    
            }

            Uri = uri;
        }

        public string AbsolutePath => Uri.AbsolutePath;
    }
}
