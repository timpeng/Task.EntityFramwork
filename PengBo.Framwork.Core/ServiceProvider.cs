using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using PengBo.Framwork.Core;
using PengBo.Framwork.Domain;
using PengBo.Framwork.Unity;

[assembly: PreApplicationStartMethod(typeof(ServiceProvider), "Init")]
namespace PengBo.Framwork.Core
{
    public class ServiceProvider : IDisposable
    {
        private const string Repositorydll = "PengBo.Framwork.Repository.dll";
        private static IContainer _container;
        public static void Init()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var assembly = Assembly.UnsafeLoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", Repositorydll));
            RegsiterService(builder, assembly);
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
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
            return _container.Resolve<T>(parameters);
        }
        public static T ResloveNamed<T>(string name)
        {
            return _container.ResolveNamed<T>(name);
        }
        private static void RegsiterService(ContainerBuilder builder, Assembly assembly)
        {
            //注入的对象不能为接口或者抽象类
            var types = assembly.GetTypes().Where(u => u.IsClass && !u.IsAbstract);
            types.ForEach(u =>
            {
                var contract = u.GetInterfaces()
                    .Where(s => s.Name.EndsWith("Repository") || s
                    .Name.EndsWith("Service")).ToArray();
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
            _container.Dispose();
        }
    }
}
