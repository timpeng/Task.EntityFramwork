using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using PengBo.Framwork.Core;
using PengBo.Framwork.Domain;
using PengBo.Framwork.Unity;

//[assembly: PreApplicationStartMethod(typeof(ServiceProvider), "Init")]
//已经由WCF主机初始化，若非WCF调用请启用
namespace PengBo.Framwork.Core
{
    public class ServiceProvider : IDisposable
    {

        public static void Init()
        {
            const string repositorydll = "PengBo.Framwork.Repository.dll";
            const string servicedll = "PengBo.Framwork.Wcf.Service.dll";
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var repositorydllassembly = Assembly.UnsafeLoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", repositorydll));
            var servicedllassembly = Assembly.UnsafeLoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", servicedll));
            RegsiterService(builder, repositorydllassembly);
            RegsiterService(builder, servicedllassembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //把当前容器放入缓存
            if (CacheManager.GetCache<IContainer>(Fields.ContainerCache) == null)
            {
                CacheManager.Current[Fields.ContainerCache] = container;
            }
        }

        public static IContainer Container
        {
            get
            {
                return CacheManager.GetCache<IContainer>(Fields.ContainerCache);
            }
        }
        /// <summary>
        /// 注入入口，这里支持多个EF请求上下文的注入，这里默认注入的是Tbl_SoaEntities库
        /// 具体注入对象见：EF上下文工厂
        /// </summary>
        public static T Reslove<T>(Parameter parameters = null)//todo:NamedParameter ,TypedParameter ,ResolvedParameter 
        {
            if (parameters == null)
            {
                parameters = DbContextFactory<Tbl_SoaEntities>.Current.Operator;//把当前EF对象作为传入。
            }
            return Container.Resolve<T>(parameters);
        }
        public static T ResloveNamed<T>(string name)
        {
            return Container.ResolveNamed<T>(name);
        }
        private static void RegsiterService(ContainerBuilder builder, Assembly assembly)
        {
            //注入的对象不能为接口或者抽象类
            var types = assembly.GetTypes().Where(u => u.IsClass && !u.IsAbstract);
            types.ForEach(u =>
            {
                var contract = u.GetInterfaces()
                    .Where(s => (s.Name.EndsWith("Repository") || s
                    .Name.EndsWith("Service")) && !s.Name.StartsWith("IBase")).ToArray();
                if (!u.IsGenericType)
                {
                    builder.RegisterType(u).As(contract).Named(u.Name, u);
                }
                else
                {
                    builder.RegisterGeneric(u).As(contract).Named(u.Name, u);
                }
            });
        }
        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
