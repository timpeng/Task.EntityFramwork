using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using PengBo.Framwork.Domain;

namespace PengBo.Framwork.Core
{
    /// <summary>
    /// EF请求上下文工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbContextFactory<T> where T : class
    {
        private volatile static DbContextFactory<T> _instance;
        public static readonly object Lock = new object();
        public static DbContextFactory<T> Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DbContextFactory<T>();
                        }
                    }
                }
                return _instance;
            }
        }
        private Parameter _operator;
        public Parameter Operator
        {
            get
            {
                if (_operator == null)
                {
                    return new TypedParameter(typeof(DbContext),Activator.CreateInstance<T>());
                }
                return _operator;
            }
            set { _operator = value; }
        }
    }
}
