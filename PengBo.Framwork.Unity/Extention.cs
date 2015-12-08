using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExpressionSerialization;
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

        public static T SerializeObject<T>(this string value) where T:class 
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string DeserializeObjectToJson<T>(this T t) where T : class,new()
        {
            return JsonConvert.SerializeObject(t);
        }

        #region 序列化System.Linq.Expression
        public static XElement Serialize<T, TS>(this Expression<Func<T, TS>> expression)
        {
            var knownTypes = new List<Type> { typeof(T) };
            var serializer = CreateSerializer(knownTypes);
            return serializer.Serialize(expression);
        }
        public static Expression<Func<T, TS>> Deserialize<T, TS>(this XElement xElement)
        {
            var knownTypes = new List<Type> { typeof(T) };
            var serializer = CreateSerializer(knownTypes);
            return serializer.Deserialize<Func<T, TS>>(xElement);
        }
        private static ExpressionSerializer CreateSerializer(List<Type> knownTypes = null)
        {
            if (knownTypes == null || !knownTypes.Any())
            {
                return new ExpressionSerializer();
            }
            var assemblies = new List<Assembly> { typeof(ExpressionType).Assembly, typeof(IQueryable).Assembly };
            knownTypes.ForEach(type => assemblies.Add(type.Assembly));
            var resolver = new TypeResolver(assemblies, knownTypes);
            var knownTypeConverter = new KnownTypeExpressionXmlConverter(resolver);
            var serializer = new ExpressionSerializer(resolver, new CustomExpressionXmlConverter[] { knownTypeConverter });
            return serializer;
        }
        #endregion
    }
}
