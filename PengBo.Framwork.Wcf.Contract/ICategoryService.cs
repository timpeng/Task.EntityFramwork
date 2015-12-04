using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PengBo.Framwork.Domain;

namespace PengBo.Framwork.Wcf.Contract
{
    [ServiceContract]
    public interface ICategoryService : IBaseService<Category>
    {

    }
}
