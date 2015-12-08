using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace PengBo.Framwork.Wcf.Common
{
    public class WebClientParameterInspector:IParameterInspector
    {
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            throw new NotImplementedException();
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            throw new NotImplementedException();
        }
    }
}
