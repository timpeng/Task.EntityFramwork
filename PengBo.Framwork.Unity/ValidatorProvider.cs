using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PengBo.Framwork.Unity
{
    public class ValidatorProvider
    {
        public static void Validator<T>(T t, string tips, Func<T, bool> func)
        {
            if (func != null)
            {
                if (func(t))
                {
                    throw new ArgumentException(tips);
                }
            }
        }
    }
}
