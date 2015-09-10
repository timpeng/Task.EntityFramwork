using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PengBo.Framwork.Unity
{
    public static class Extention
    {
        public static void ForEach<T>(this IEnumerable<T> value, Action<T> action)
        {
            foreach (var key in value)
            {
                action(key);
            }
        }

        public static string Append(this string value, string append)
        {
            return value + append;
        }
    }
}
