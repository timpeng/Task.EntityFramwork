using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PengBo.Framwork.Core;
using PengBo.Framwork.Domain;
using PengBo.Framwork.IRepository;
using PengBo.Framwork.Wcf.Contract;

namespace PengBo.Framwork.Wcf.Service
{
    public class TestService : BaseService<Test>, ITestService
    {
        private IBaseRepository<Test> _baseRepository;
        public IBaseRepository<Test> BaseRepository
        {
            set
            {
                _baseRepository = value;
            }
            get
            {
                return _baseRepository ?? ServiceProvider.Reslove<ITestRepository>(DbContextFactory<Tbl_TestEntities>.Current.Operator);
            }
        }
        public override IBaseRepository<Test> DataContext
        {
            get { return BaseRepository; }
        }

        public void Dispose()
        {
            BaseRepository.Dispose();
        }
    }
}
