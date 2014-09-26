using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServTK.Helpers
{
    public static class StringCompare
    {
        private static readonly IComparer _comparer = CaseInsensitiveComparer.Default;

        public static IComparer Comparer
        {
            get { return _comparer; }
        }

        public static bool Equals(string a, string b)
        {
            if (a == null && b == null)
                return true;

            if (a == null || b == null || a.Length != b.Length)
                return false;

            return (_comparer.Compare(a, b) == 0);
        }

        public static bool StartsWith(string a, string b)
        {
            if (a == null || b == null || a.Length < b.Length)
                return false;

            return (_comparer.Compare(a.Substring(0, b.Length), b) == 0);
        }

        public static bool EndsWith(string a, string b)
        {
            if (a == null || b == null || a.Length < b.Length)
                return false;
            

            return (_comparer.Compare(a.Substring(a.Length - b.Length), b) == 0);
        }

        public static bool Contains(string a, string b)
        {
            if (a == null || b == null || a.Length < b.Length)
                return false;
            
            a = a.ToLower();
            b = b.ToLower();

            return (a.IndexOf(b) >= 0);
        }
    }
}
