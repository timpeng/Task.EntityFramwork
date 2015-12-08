using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PengBo.Framwork.Wcf.Common
{
    public class GlobalConstants
    {
        public const string CallContextKey = "__WCFCONTEXT";
        public const string ContextHeaderLocalName = "__WCFCONTEXT";
        public const string ContextHeaderNameSpace = "www.pengbo123.com";
        public const string WcfOperatorKey = "__WCFOPERATOR";
    }

    public class WcfOperator
    {
        public string IpAddress { get; set; }
        public string UId { get; set; }
        public DateTime StartTime { get; set; }
        public Guid LoginToken { get; set; }
        public string Action { get; set; }
    }
}
