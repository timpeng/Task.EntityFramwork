using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations.History;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using PengBo.Framwork.Core;
using PengBo.Framwork.Domain;
using PengBo.Framwork.Unity;

namespace PengBo.Framwork.Repository
{
    public class DbContextRepository<T> : IDisposable where T : class ,new()
    {
        public DbContextRepository(DbContext db)
        {
            this.Db = db;
        }
        #region 初始化字段
        /// <summary>
        /// 开始事物
        /// </summary>
        public void Tran_Begin(Action action)
        {
            using (var dbTransaction = new TransactionScope())
            {
                try
                {
                    if (action != null)
                    {
                        action();
                        dbTransaction.Complete();
                    }
                }
                catch (Exception exception)
                {
                    var s = exception.ToString(); //todo：记录异常
                }
            }
        }
        public DbContext Db { get; set; }
        private DbSet<T> _dbSet;
        private DbSet<T> DbSet
        {
            get
            {
                if (_dbSet == null)
                {
                    return Db.Set<T>();
                }
                return _dbSet;
            }
            set { _dbSet = value; }
        }
        #endregion
        #region 保存
        public async Task<bool> SaveChangeAsync()
        {
            //todo:可以在此执行自己的操作例如log记录等
            using (this)
            {
                return await Db.SaveChangesAsync() > 0;
            }
        }
        public bool SaveChanges()
        {
            //todo:可以在此执行自己的操作例如log记录等
            using (this)
            {
                return Db.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 批量保存-这个按需求来调用
        /// </summary>
        /// <returns></returns>
        public async Task BulkSaveChangeAsync()
        {
            await Task.Run(() => Db.BulkSaveChanges());
        }
        #endregion
        #region 删除
        public bool Delete(T model)
        {
            DbSet.Attach(model);
            DbSet.Remove(model);
            return this.SaveChanges();
        }
        public async Task<bool> DeleteAsync(T model)
        {
            DbSet.Attach(model);
            DbSet.Remove(model);
            return await this.SaveChangeAsync();
        }
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> wherExpression)
        {
            //todo:这里的性能不是很好，还在优化中
            var result = DbSet.Where(wherExpression);
            await this.DeleteAsync(result);
            return await this.SaveChangeAsync();
        }
        public async Task<bool> DeleteAsync(IEnumerable<T> model)
        {
            await Task.Run(() => this.BulkDelete(model));
            return await this.SaveChangeAsync();
        }
        public void BulkDelete(IEnumerable<T> model)
        {
            Db.BulkDelete(model);
        }
        #endregion
        #region 添加
        public async Task<bool> InsertAsync(T model)
        {
            Db.Set<T>().Add(model);
            return await this.SaveChangeAsync();
        }
        public bool Insert(T model)
        {
            DbSet.Add(model);
            return this.SaveChanges();
        }
        public async Task<bool> InsertAsync(IEnumerable<T> model)
        {
            await Task.Run(() => this.BulkInsert(model));
            return await this.SaveChangeAsync();
        }
        public void BulkInsert(IEnumerable<T> model)
        {
            Db.BulkInsert(model);
        }
        #endregion
        #region 更新
        public bool Update(T model)
        {
            DbSet.Attach(model);
            Db.Entry(model).State = EntityState.Modified;
            return this.SaveChanges();
        }
        public async Task<bool> UpdateAsync(T model)
        {
            DbSet.Attach(model);
            Db.Entry(model).State = EntityState.Modified;
            return await this.SaveChangeAsync();
        }
        /// <summary>
        /// 对于某些字段进行更新
        /// </summary>
        /// <param name="model">要更新的实体对象</param>
        /// <param name="param">要更新的字段名称</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T model, params string[] param)
        {
            DbSet.Attach(model);
            param.ForEach(s =>
            {
                Db.Entry(model).Property(s).IsModified = true;
            });
            return await this.SaveChangeAsync();
        }
        public void BulkUpdate(IEnumerable<T> model)
        {
            Db.BulkUpdate(model);
        }
        #endregion
        #region 查找

        public async Task<IQueryable<T>> GetEntitiesAsync(Expression<Func<T, bool>> wherExpression)
        {
            return await Task.Run(() => DbSet.Where(wherExpression));
        }
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> wherExpression)
        {
            return await Task.Run(() => DbSet.Where(wherExpression).FirstOrDefault());
        }
        public async Task<IQueryable<T>> GetEntitiesAsync()
        {
            return await Task.Run(() => GetEntitiesAsync(u => true));
        }
        public async Task<T> GetEntityAsync()
        {
            return await Task.Run(() => GetEntityAsync(u => true));
        }
        /// <summary>
        /// 数据条数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountAsync()
        {
            return await Task.Run(() => DbSet.Count());
        }
        /// <summary>
        /// 数据是否为空
        /// </summary>
        public  bool IsEmpty
        {
            get { return DbSet.Any(); }
        }
        public IQueryable<T> GetEntitiesPaging<TS>(Expression<Func<T, bool>> wherExpression, Expression<Func<T, TS>> orderExpression, int size, int index, out double pages, out int total)
        {
            var data = DbSet.Where(wherExpression);
            total = data.Count();
            pages = Math.Ceiling(total * 1.0 / size);
            return data.OrderByDescending(orderExpression)//todo:OrderBy自己补充
                .Skip(size * (index - 1))
                .Take(size);
        }
        #endregion
        #region 执行 Add  Update Remove--sql

        public async Task<bool> ExecuteSqlCommandAsync(string sql, params object[] param)
        {
            using (this)
            {
                return await Db.Database.ExecuteSqlCommandAsync(sql, param) > 0;
            }
        }
        #endregion
        #region 执行 select--sql
        public async Task<IQueryable<T>> SqlQueryAsync(string sql, params object[] param)
        {
            return await Task.Run(() => Db.Database.SqlQuery<T>(sql, param).AsQueryable());
        }
        #endregion
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
