using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PengBo.Framwork.Wcf.Contract
{
    [ServiceContract]
    public interface IBaseService<T> :IDisposable where T : class
    {
        [OperationContract]
        void Tran_Begin(Action action);
        [OperationContract]
        bool IsEmpty();
        [OperationContract]
        void Insert(T model);
        [OperationContract]
        void Delete(T model);
        [OperationContract]
        void Update(T model);
        [OperationContract]
        Task<int> GetCountAsync();
        [OperationContract]

        Task<bool> SaveChangeAsync();
        [OperationContract]
        Task BulkSaveChangeAsync();
        [OperationContract]
        bool SaveChanges();
        [OperationContract]
        Task<bool> DeleteAsync(T model);
        [OperationContract]

        Task<bool> DeleteAsync(IEnumerable<T> model);
        [OperationContract]

        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);
        [OperationContract]
        void BulkDelete(IEnumerable<T> model);
        [OperationContract]
        Task<bool> InsertAsync(T model);
        [OperationContract]
        Task<bool> InsertAsync(IEnumerable<T> model);
        [OperationContract]
        void BulkInsert(IEnumerable<T> model);
        [OperationContract]
        Task<bool> UpdateAsync(T model);
        [OperationContract]
        Task<bool> UpdateAsync(T model, params string[] param);
        [OperationContract]
        void BulkUpdate(IEnumerable<T> model);
        [OperationContract]
        Task<IQueryable<T>> GetEntitiesAsync(Expression<Func<T, bool>> whereExpression);
        [OperationContract]
        Task<T> GetEntityAsync(Expression<Func<T, bool>> whereExpression);
        [OperationContract]
        Task<IQueryable<T>> GetEntitiesAsync();
        [OperationContract]
        Task<T> GetEntityAsync();
        [OperationContract]
        IQueryable<T> GetEntitiesPaging<TS>(Expression<Func<T, bool>> wherExpression,
            Expression<Func<T, TS>> orderExpression, int size, int index, out double pages, out int total);
        [OperationContract]
        Task<bool> ExecuteSqlCommandAsync(string sql, params object[] param);
        [OperationContract]
        Task<IQueryable<T>> SqlQueryAsync(string sql, params object[] param);
    }
}
