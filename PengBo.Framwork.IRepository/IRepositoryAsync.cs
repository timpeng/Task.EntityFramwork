using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PengBo.Framwork.IRepository
{
    public interface IRepositoryAsync<T>:IDisposable where T : class,new()
    {
        DbContext Db { get; set; }
        void Tran_Begin(Action action);
        bool IsEmpty { get; }
        void Insert(T model);
        void Delete(T model);
        void Update(T model);
        Task<int> GetCountAsync();

        Task<bool> SaveChangeAsync();

        Task BulkSaveChangeAsync();
        bool SaveChanges();
        Task<bool> DeleteAsync(T model);

        Task<bool> DeleteAsync(IEnumerable<T> model);

        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);

        void BulkDelete(IEnumerable<T> model);

        Task<bool> InsertAsync(T model);

        Task<bool> InsertAsync(IEnumerable<T> model);

        void BulkInsert(IEnumerable<T> model);

        Task<bool> UpdateAsync(T model);

        Task<bool> UpdateAsync(T model, params string[] param);

        void BulkUpdate(IEnumerable<T> model);

        Task<IQueryable<T>> GetEntitiesAsync(Expression<Func<T, bool>> whereExpression);

        Task<T> GetEntityAsync(Expression<Func<T, bool>> whereExpression);

        Task<IQueryable<T>> GetEntitiesAsync();

        Task<T> GetEntityAsync();

        IQueryable<T> GetEntitiesPaging<TS>(Expression<Func<T, bool>> wherExpression,
            Expression<Func<T, TS>> orderExpression, int size, int index, out double pages, out int total);

        Task<bool> ExecuteSqlCommandAsync(string sql, params object[] param);

        Task<IQueryable<T>> SqlQueryAsync(string sql, params object[] param);
    }
}
