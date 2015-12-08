using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace PengBo.Framwork.Wcf.Common
{
    /// <summary>
    /// 实现自定义终结点行为扩展
    /// </summary>
    public class WebClientEndPointBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        public override Type BehaviorType
        {
            get { return typeof(WebClientEndPointBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new WebClientEndPointBehavior();
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new WebClientMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new WebClientMessageInspector());
        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }
    }
}
