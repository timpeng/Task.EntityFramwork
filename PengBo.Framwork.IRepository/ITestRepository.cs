using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PengBo.Framwork.Domain;

namespace PengBo.Framwork.IRepository
{
    public interface ITestRepository : IRepositoryAsync<Test>
    {
    }
}
