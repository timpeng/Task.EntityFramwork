using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using PengBo.Framwork.Unity;
using IContainer = System.ComponentModel.IContainer;

namespace PengBo.Framwork.Wcf.Host
{
    partial class WcfBindingService : ServiceBase
    {
        public WcfBindingService()
        {
            InitializeComponent();
        }
        private readonly List<ServiceHost> _list = new List<ServiceHost>();
        /// <summary>
        /// 注入仓储层对应的接口服务
        /// </summary>
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
        /// <summary>
        /// 初始化IOC注入
        /// </summary>
        private void RunServiceProvider()
        {
            const string repositorydll = "PengBo.Framwork.Repository.dll";
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var repositorydllassembly = Assembly.UnsafeLoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, repositorydll));
            RegsiterService(builder, repositorydllassembly);
            var container = builder.Build();
            //把当前容器放入缓存
            if (CacheManager.GetCache<IContainer>(Fields.ContainerCache) == null)
            {
                CacheManager.Current[Fields.ContainerCache] = container;
            }
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        internal void RunService()
        {
            var sections = ConfigurationManager.GetSection("system.serviceModel/services") as ServicesSection;
            string namespaces = ConfigurationManager.AppSettings["_namespaces"];
            if (sections != null)
            {
                var services = sections.Services;
                foreach (ServiceElement service in services)
                {
                    var type = Type.GetType(service.Name + "," + namespaces);
                    if (type != null)
                    {
                        var host = new ServiceHost(type);
                        host.Opened += host_Opened;
                        host.Opening += host_Opening;
                        _list.Add(host);
                        host.Open();
                    }
                    else
                    {
                        Console.WriteLine("{0}服务程序未提供配置", service.Name);
                    }
                }
            }
            RunServiceProvider();
        }
        void host_Opening(object sender, EventArgs e)
        {
            var host = sender as ServiceHost;
            if (host != null)
            {
                Console.WriteLine("{0}服务正在启动...", host.Description.Name);
            }
        }
        void host_Opened(object sender, EventArgs e)
        {
            var host = sender as ServiceHost;
            if (host != null)
            {
                Console.WriteLine("{0}服务启动成功!", host.Description.Name);
            }
        }
        protected override void OnStart(string[] args)
        {
            RunService();
        }
        protected override void OnStop()
        {
            _list.ForEach(s =>
            {
                if (s.State == CommunicationState.Opened)
                {
                    try
                    {
                        s.Close();
                    }
                    catch
                    {
                        s.Abort();
                    }
                }
            });
            CacheManager.RemoveCaChe(Fields.ContainerCache);
        }
    }
}
