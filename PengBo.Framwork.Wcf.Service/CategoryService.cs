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
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private IBaseRepository<Category> _baseRepository;
        public IBaseRepository<Category> BaseRepository
        {
            set
            {
                _baseRepository = value;
            }
            get
            {
                return _baseRepository ?? ServiceProvider.Reslove<ICategoryRepository>();
            }
        }

        public override IBaseRepository<Category> DataContext
        {
            get { return BaseRepository; }
        }

        public void Dispose()
        {
            _baseRepository.Dispose();
        }
    }
}
