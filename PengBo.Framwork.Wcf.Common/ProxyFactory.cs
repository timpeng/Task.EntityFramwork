using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace PengBo.Framwork.Wcf.Common
{
    public class ProxyFactory
    {
        /// <summary>
        /// 执行带返回至的接口
        /// </summary>
        public static TS GetProxy<T, TS>(Func<T, TS> funcHandler) where TS : class
        {
            TS ts = null;
            using (var proxy = new ChannelFactory<T>(typeof(T).Name))
            {
                var t = proxy.CreateChannel();
                var chanel = ((IChannel)t);
                try
                {
                    chanel.Open();
                    if (funcHandler != null)
                    {
                        ts = funcHandler(t);
                    }
                }
                finally
                {
                    if (chanel.State == CommunicationState.Opened)
                    {
                        ((IContextChannel)t).OperationTimeout = new TimeSpan(0, 5, 0);
                    }
                    if (chanel.State == CommunicationState.Faulted)
                    {
                        chanel.Abort();
                    }
                    chanel.Close();
                }
            }
            return ts;
        }
        /// <summary>
        /// 执行任意接口
        /// </summary>
        public static void GetProxy<T>(Action<T> actionHandler) where T : class
        {
            T t;
            using (var proxy = new ChannelFactory<T>(typeof(T).Name))
            {
                t = proxy.CreateChannel();
                var chanel = ((IChannel)t);
                try
                {
                    chanel.Open();
                    if (actionHandler != null)
                    {
                        actionHandler(t);
                    }
                }
                finally
                {
                    if (chanel.State == CommunicationState.Opened)
                    {
                        ((IContextChannel)t).OperationTimeout = new TimeSpan(0, 5, 0);
                    }
                    if (chanel.State == CommunicationState.Faulted)
                    {
                        chanel.Abort();
                    }
                    chanel.Close();
                }
            }
        }
    }
}
