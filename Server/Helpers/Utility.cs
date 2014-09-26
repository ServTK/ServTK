using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServTK.Helpers
{
    public static class Utility
    {
        public static void Separate(StringBuilder sb, string value, string separator)
        {
            if (sb.Length > 0)
            {
                sb.Append(separator);
            }

            sb.Append(value);
        }
    }
}
