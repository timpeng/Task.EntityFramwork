using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace PengBo.Framwork.Wcf.Common
{
    /// <summary>
    /// 实现自定义客户端消息拦截器
    /// 和服务端消息拦截器
    /// </summary>
    public class WebClientMessageInspector : IDispatchMessageInspector, IClientMessageInspector
    {
        /// <summary>
        /// 服务端开始接受消息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            var context = request.Headers.GetHeader<WcfContext>(GlobalConstants.ContextHeaderLocalName,
                 GlobalConstants.ContextHeaderNameSpace);
            //这里请自定义验证方式。
            //if (context.Operator.LoginToken.ToString()!="8888888")
            //{
            //    throw new Exception("您的token验证失败,请联系服务提供商!");
            //}
            return null;
        }
        /// <summary>
        /// 服务端接受消息完毕，准备发送消息
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }
        /// <summary>
        /// 客户端回复消息之后
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
        }
        /// <summary>
        /// 接受客户端消息之前
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            var header = MessageHeader.CreateHeader(GlobalConstants.ContextHeaderLocalName, GlobalConstants.ContextHeaderNameSpace,
                   WcfContext.Current);
            if (request != null)
            {
                request.Headers.Add(header);
            }
            return null;
        }
    }
}
