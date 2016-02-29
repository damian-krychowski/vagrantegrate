using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vagrantegrate
{
    internal static class UriExtensions
    {
        public static Uri FileLocationUri(this Uri uri)
        {
            if (!uri.IsFile)
            {
                throw new InvalidOperationException("Uri does not represent a file.");
            }

            return new Uri(uri, ".");
        }

        public static string LocationPathRelativeToRoot(this Uri uri) 
        {
            var locationUri = uri.FileLocationUri();
            return locationUri.AbsolutePath.Replace(locationUri.Segments[1], "/");
        }

        public static bool IsFileLocatedInRoot(this Uri uri)
        {
            return uri.Segments.Length <= 3;
        }

        public static bool IsVagrant(this Uri uri)
        {
            return uri.Scheme == "vagrant";
        }
    }

    internal static class VagrantUriExtensions
    {
        public static VagrantUri FileLocationUri(this VagrantUri vagrantUri)
        {
            if (!vagrantUri.Uri.IsVagrant())
            {
                throw new InvalidOperationException("Uri does not represent a file.");
            }

            return new VagrantUri(new Uri(vagrantUri.Uri, "."));
        }

        public static string LocationPathRelativeToRoot(this VagrantUri vagrantUri)
        {
            var locationUri = vagrantUri.FileLocationUri();
            return locationUri.AbsolutePath.Replace(locationUri.Uri.Segments[0], "");
        }

        public static bool IsFileLocatedInRoot(this VagrantUri vagrantUri)
        {
            return vagrantUri.Uri.Segments.Length <= 2;
        }
    }
}
