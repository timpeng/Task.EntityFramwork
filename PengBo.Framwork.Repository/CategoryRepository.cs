﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PengBo.Framwork.Domain;
using PengBo.Framwork.IRepository;

namespace PengBo.Framwork.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext db)//继承基类构造函数，由入口传入参数
            : base(db)
        {

        }
    }
}
