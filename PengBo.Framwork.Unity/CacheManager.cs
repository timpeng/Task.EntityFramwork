using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace PengBo.Framwork.Unity
{
    public class CacheManager
    {
        #region 普通缓存

        public static CacheManager Current
        {
            get
            {
                return new CacheManager();
            }
        }
        public static Cache CacheContext
        {
            get
            {
                return HttpRuntime.Cache;
            }
        }

        public object this[string key]
        {
            set
            {
                if (CacheContext != null)
                {
                    CacheContext[key] = value;
                }
            }
        }

        public static T GetCache<T>(string key) where T : class
        {
            if (CacheContext != null)
            {
                return CacheContext.Get(key) as T;
            }
            return null;
        }

        public static bool RemoveCaChe(string key)
        {
            if (GetCache<object>(key) != null && CacheContext != null)
            {
                CacheContext.Remove(key);
                return true;
            }
            return false;
        }
        #endregion
    }
}
