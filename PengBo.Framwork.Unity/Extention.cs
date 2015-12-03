using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        public static string TryToString(this object value)
        {
            return Convert.ToString(value);
        }
        public static int TryToInt(this object value)
        {
            int num;
            if (int.TryParse(value.TryToString(), out num))
            {
                return num;
            }
            throw new Exception("当前值不能转为int32");
        }

        public static T DeserializeObjectToJson<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string DeserializeObjectToJson<T>(this T t) where T : class,new()
        {
            return JsonConvert.SerializeObject(t);
        }
    }
}
