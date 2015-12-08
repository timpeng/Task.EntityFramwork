using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PengBo.Framwork.IRepository;
using PengBo.Framwork.Unity;

namespace PengBo.Framwork.Wcf.Service
{
    public abstract class BaseService<T> where T : class ,new()
    {
        public abstract IBaseRepository<T> DataContext { get; }
        public void Tran_Begin(Action action)
        {
            DataContext.Tran_Begin(action);
        }
        #region 删除
        public bool Delete(T model)
        {
            return DataContext.Delete(model);
        }
        public async Task<bool> DeleteAsync(T model)
        {
            return await DataContext.DeleteAsync(model);
        }
        public async Task<bool> DeleteAsync(XElement xElement)
        {
            return await DataContext.DeleteAsync(xElement.Deserialize<T, bool>());
        }
        public async Task<bool> DeleteAsync(IEnumerable<T> model)
        {
            return await DataContext.DeleteAsync(model);
        }
        public async Task BulkDelete(IEnumerable<T> model)
        {
            await DataContext.BulkDelete(model);
        }
        #endregion
        #region 添加
        public async Task<bool> InsertAsync(T model)
        {
            return await DataContext.InsertAsync(model);
        }
        public bool Insert(T model)
        {
            return DataContext.Insert(model);
        }
        public async Task<bool> InsertAsync(IEnumerable<T> model)
        {
            return await DataContext.InsertAsync(model);
        }
        public async Task BulkInsert(IEnumerable<T> model)
        {
            await DataContext.BulkInsert(model);
        }
        #endregion
        #region 更新
        public bool Update(T model)
        {
            return DataContext.Update(model);
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
        public async Task BulkUpdate(IEnumerable<T> model)
        {
            await DataContext.BulkUpdate(model);
        }
        #endregion
        #region 查找

        public async Task<IQueryable<T>> GetEntitiesAsync(XElement xElement)
        {
            return await DataContext.GetEntitiesAsync(xElement.Deserialize<T, bool>());
        }
        public async Task<T> GetEntityAsync(XElement xElement)
        {
            return await DataContext.GetEntityAsync(xElement.Deserialize<T, bool>());
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
        public IQueryable<T> GetEntitiesPaging<TS>(XElement xElementWhere, XElement xElementOrder, int size, int index, out double pages, out int total)
        {
            return DataContext.GetEntitiesPaging(xElementWhere.Deserialize<T, bool>(), xElementOrder.Deserialize<T, TS>(), size, index, out pages, out total);
        }
        #endregion
        #region 执行 Add  Update Remove--sql

        public async Task<bool> ExecuteSqlCommandAsync(string sql, params object[] param)
        {
            return await DataContext.ExecuteSqlCommandAsync(sql, param);
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
