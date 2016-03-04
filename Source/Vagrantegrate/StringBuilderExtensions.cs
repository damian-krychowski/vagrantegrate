using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vagrantegrate
{
    internal static class StringBuilderExtensions
    {
        public static StringBuilder AppendLowerCaseLine(this StringBuilder builder, string line)
        {
            return builder.AppendLine(line.ToLower());
        }
    }
}
