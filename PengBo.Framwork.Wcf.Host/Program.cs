using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace PengBo.Framwork.Wcf.Host
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
#if !DEBUG
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new WcfBindingService() 
            };
            ServiceBase.Run(ServicesToRun);
#else
            var service = new WcfBindingService();
            service.RunService();
            Console.WriteLine("主机正在运行中...");
            Console.WriteLine("请按任意键退出.");
            Console.ReadKey(true);
#endif
        }
    }
}
