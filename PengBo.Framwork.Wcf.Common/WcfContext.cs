using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PengBo.Framwork.Unity;

namespace PengBo.Framwork.Wcf.Common
{
    [Serializable]
    public class WcfContext : Dictionary<string, object>
    {
        private void EnsureSerializable(object value)
        {
            if (value == null)
            {
                throw new ArgumentException("value is not null.");
            }
            if (!value.GetType().IsSerializable)
            {
                throw new ArgumentException(string.Format("The argument of the type {0} is not serializable!", value.GetType().FullName));
            }
        }
        public new object this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                EnsureSerializable(value);
                base[key] = value;
            }
        }
        public static WcfContext Current
        {
            get
            {
                if (CacheManager.GetCache<WcfContext>(GlobalConstants.CallContextKey) == null)
                {
                    CacheManager.Current[GlobalConstants.CallContextKey] = new WcfContext();
                }
                return CacheManager.GetCache<WcfContext>(GlobalConstants.CallContextKey);
            }
            set
            {
                CacheManager.Current[GlobalConstants.CallContextKey] = value;
            }
        }
        public  WcfOperator Operator
        {
            get
            {
                return this[GlobalConstants.WcfOperatorKey].TryToString().SerializeObject<WcfOperator>();
            }
            set
            {
                this[GlobalConstants.WcfOperatorKey] = value.DeserializeObjectToJson();
            }
        }
    }
}
