using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vagrantegrate
{
    internal static class UriExtensions
    {
        public static Uri FolderUri(this Uri uri)
        {
            if (!uri.IsFile)
            {
                throw new InvalidOperationException("Uri does not represent a file.");
            }

            return new Uri(uri, ".");
        }

        public static string LocationPathRelativeToRoot(this Uri uri) 
        {
            var locationUri = uri.FolderUri();
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

        public static void ThrowIfFileDoesntExist(this Uri uri)
        {
            if (!uri.IsFile)
            {
                throw new InvalidOperationException("Uri doesn't represents the file.");
            }

            if (!File.Exists(uri.AbsolutePath))
            {
                throw new FileNotFoundException(uri.ToString());
            }
        }

        public static void ThrowIfFolderDoesntExist(this Uri uri)
        {
            if (!Directory.Exists(uri.AbsolutePath))
            {
                throw new DirectoryNotFoundException(uri.ToString());
            }
        }
    }

    internal static class VagrantUriExtensions
    {
        public static VagrantUri FolderUri(this VagrantUri vagrantUri)
        {
            if (!vagrantUri.Uri.IsVagrant())
            {
                throw new InvalidOperationException("Uri does not represent a file.");
            }

            return new VagrantUri(new Uri(vagrantUri.Uri, "."));
        }

        public static string LocationPathRelativeToRoot(this VagrantUri vagrantUri)
        {
            var locationUri = vagrantUri.FolderUri();
            return locationUri.AbsolutePath.Replace(locationUri.Uri.Segments[0], "");
        }

        public static bool IsFileLocatedInRoot(this VagrantUri vagrantUri)
        {
            return vagrantUri.Uri.Segments.Length <= 2;
        }
    }
}
