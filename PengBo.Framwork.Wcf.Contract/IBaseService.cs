using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PengBo.Framwork.Wcf.Contract
{
    [ServiceContract]
    public interface IBaseService<T> : IDisposable where T : class
    {
        [OperationContract]
        void Tran_Begin(Action action);
        [OperationContract]
        bool IsEmpty();
        [OperationContract]
        bool Insert(T model);
        [OperationContract]
        bool Delete(T model);
        [OperationContract]
        bool Update(T model);
        [OperationContract(Name = "TaskGetCount")]
        Task<int> GetCountAsync();

        [OperationContract(Name = "TaskDelete")]
        Task<bool> DeleteAsync(T model);
        [OperationContract(Name = "TaskDeleteBulk")]

        Task<bool> DeleteAsync(IEnumerable<T> model);
        [OperationContract(Name = "TaskDeleteByLambda")]

        Task<bool> DeleteAsync(XElement xElement);
        [OperationContract]
        Task BulkDelete(IEnumerable<T> model);
        [OperationContract(Name = "TaskInsert")]
        Task<bool> InsertAsync(T model);
        [OperationContract(Name = "TaskInsertBulk")]
        Task<bool> InsertAsync(IEnumerable<T> model);
        [OperationContract]
        Task BulkInsert(IEnumerable<T> model);
        [OperationContract(Name = "TaskUpdate")]
        Task<bool> UpdateAsync(T model);
        [OperationContract(Name = "TaskUpdateSeparateField")]
        Task<bool> UpdateAsync(T model, params string[] param);
        [OperationContract]
        Task BulkUpdate(IEnumerable<T> model);
        [OperationContract(Name = "TaskGetEntitiesByLambda")]
        Task<IQueryable<T>> GetEntitiesAsync(XElement xElement);
        [OperationContract(Name = "TaskGetEntityByLambda")]
        Task<T> GetEntityAsync(XElement xElement);
        [OperationContract(Name = "TaskGetEntities")]
        Task<IQueryable<T>> GetEntitiesAsync();
        [OperationContract(Name = "TaskGetEntity")]
        Task<T> GetEntityAsync();
        [OperationContract]
        IQueryable<T> GetEntitiesPaging<TS>(XElement xElementWhere, XElement xElementOrder, int size, int index, out double pages, out int total);
        [OperationContract(Name = "TaskExecuteSqlCommand")]
        Task<bool> ExecuteSqlCommandAsync(string sql, params object[] param);
        [OperationContract(Name = "TaskSqlQuery")]
        Task<IQueryable<T>> SqlQueryAsync(string sql, params object[] param);
    }
}
