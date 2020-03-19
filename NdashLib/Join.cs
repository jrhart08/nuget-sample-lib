using System;
using System.Collections.Generic;
using System.Text;

namespace NdashLib
{
    public static partial class Ndash
    {
        public static string Join<T>(this IEnumerable<T> collection, string separator = ",")
        {
            return string.Join(separator, collection);
        }
    }
}
