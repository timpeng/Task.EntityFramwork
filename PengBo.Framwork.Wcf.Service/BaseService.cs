using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PengBo.Framwork.IRepository;

namespace PengBo.Framwork.Wcf.Service
{
    public abstract class BaseService<T> where T : class ,new()
    {
        public abstract IBaseRepository<T> DataContext { get; }
        public void Tran_Begin(Action action)
        {
            DataContext.Tran_Begin(action);
        }
        #region 保存
        public async Task<bool> SaveChangeAsync()
        { 
            return await DataContext.SaveChangeAsync();
        }
        public bool SaveChanges()
        {
            return  DataContext.SaveChanges();
        }
        /// <summary>
        /// 批量保存-这个按需求来调用
        /// </summary>
        /// <returns></returns>
        public async Task BulkSaveChangeAsync()
        {
            await DataContext.BulkSaveChangeAsync();
        }
        #endregion
        #region 删除
        public void Delete(T model)
        {
            DataContext.Delete(model);
        }
        public async Task<bool> DeleteAsync(T model)
        {
            return await DataContext.DeleteAsync(model);
        }
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> wherExpression)
        {
            return await DataContext.DeleteAsync(wherExpression);
        }
        public async Task<bool> DeleteAsync(IEnumerable<T> model)
        {
            return await DataContext.DeleteAsync(model);
        }
        public void BulkDelete(IEnumerable<T> model)
        {
            DataContext.BulkDelete(model);
        }
        #endregion
        #region 添加
        public async Task<bool> InsertAsync(T model)
        {
            return await DataContext.InsertAsync(model);
        }
        public void Insert(T model)
        {
            DataContext.Insert(model);
        }
        public async Task<bool> InsertAsync(IEnumerable<T> model)
        {
            return await DataContext.InsertAsync(model);
        }
        public void BulkInsert(IEnumerable<T> model)
        {
            DataContext.BulkInsert(model);
        }
        #endregion
        #region 更新
        public void Update(T model)
        {
            DataContext.Update(model);
        }
        public async Task<bool> UpdateAsync(T model)
        {
            return await DataContext.UpdateAsync(model);
        }
        /// <summary>
        /// 对于某些字段进行更新
        /// </summary>
        /// <param name="model">要更新的实体对象</param>
        /// <param name="param">要更新的字段名称</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T model, params string[] param)
        {
            return await DataContext.UpdateAsync(model, param);
        }
        public void BulkUpdate(IEnumerable<T> model)
        {
            DataContext.BulkUpdate(model);
        }
        #endregion
        #region 查找

        public async Task<IQueryable<T>> GetEntitiesAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await DataContext.GetEntitiesAsync(whereExpression);
        }
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await DataContext.GetEntityAsync(whereExpression);
        }
        public async Task<IQueryable<T>> GetEntitiesAsync()
        {
            return await DataContext.GetEntitiesAsync();
        }
        public async Task<T> GetEntityAsync()
        {
            return await DataContext.GetEntityAsync();
        }
        /// <summary>
        /// 数据条数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountAsync()
        {
            return await DataContext.GetCountAsync();
        }
        /// <summary>
        /// 数据是否为空
        /// </summary>
        public bool IsEmpty()
        {
            return DataContext.IsEmpty(); 
        }
        public IQueryable<T> GetEntitiesPaging<TS>(Expression<Func<T, bool>> wherExpression, Expression<Func<T, TS>> orderExpression, int size, int index, out double pages, out int total)
        {
            return DataContext.GetEntitiesPaging(wherExpression,orderExpression,size,index,out pages,out total);
        }
        #endregion
        #region 执行 Add  Update Remove--sql

        public async Task<bool> ExecuteSqlCommandAsync(string sql, params object[] param)
        {
            return await DataContext.ExecuteSqlCommandAsync(sql,param);
        }
        #endregion
        #region 执行 select--sql
        public async Task<IQueryable<T>> SqlQueryAsync(string sql, params object[] param)
        {
            return await DataContext.SqlQueryAsync(sql, param);
        }
        #endregion
    }
}
