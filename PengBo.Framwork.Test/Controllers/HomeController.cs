using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using PengBo.Framwork.Core;
using PengBo.Framwork.Domain;
using PengBo.Framwork.IRepository;
using PengBo.Framwork.Wcf.Common;
using PengBo.Framwork.Wcf.Contract;

namespace PengBo.Framwork.Test.Controllers
{
    public class HomeController : Controller
    {
        #region field
        //private readonly ICategoryService _categoryService =
        //    ServiceProvider.Reslove<ICategoryService>();

        //private readonly ITestService _testRepository =
        //    ServiceProvider.Reslove<ITestService>(
        //    DbContextFactory<Tbl_TestEntities>.Current.Operator); 
        #endregion
        public ActionResult Index()
        {
            #region 正常调用
            //EF  在SaveChange的时候会自动调用事物，这里只是为了测试，可加入多个数据库进行测试
            //执行事务涉及到自动释放事物对象，若并行调用会出现事务锁的问题，所以这里用的是串行方式执行
            #region 执行事务
            // _categoryRepository.Tran_Begin(() =>
            //_categoryRepository.Insert(new Category
            //{
            //    Name = "跑车123456----34彭博23"
            //}));
            #endregion
            #region sql查询
            //var d = await _categoryService.SqlQueryAsync("select *  from Category");
            //ViewBag.Count = d.Count();
            //_categoryRepository.Dispose();
            #endregion
            #region 正常查询
            //var d = await _categoryRepository.GetEntityAsync(u => true);
            //var f = d.Name;
            //var d = await _categoryRepository.GetEntitiesAsync(u => true);
            //var e = d.Count();
            #endregion
            #region 增删改sql
            //bool f = await _categoryRepository.ExecuteSqlCommandAsync("delete  from Category where Id={0}", 112303);
            //Console.WriteLine(f);
            #endregion
            #region 多个库的测试
            //todo:加入事物
            //_testRepository.Tran_Begin(() =>
            //{
            //    _testRepository.Insert(new Domain.Test
            //   {
            //       Value = 5
            //   });
            //    _categoryService.Insert(new Category
            //   {
            //       Name = "彭博框架2015"
            //   });
            //});
            //_categoryRepository.Insert(new Category
            //{
            //    Name = "234234ewwer"
            //});
            //_categoryRepository.SaveChanges();
            //_testRepository.Delete(new Domain.Test
            //{
            //    Id = 1,
            //    Value = 5
            //});
            #endregion
            #endregion
            #region WCF调用
            bool result = false;
            ProxyFactory.GetProxyAny<ICategoryService>(s =>
            {
                result = s.Insert(new Category
                {
                    Name = "我是用WCF来进行操作的。。"
                });
            }
            );
            #endregion
            return View(result);
        }
    }
}
