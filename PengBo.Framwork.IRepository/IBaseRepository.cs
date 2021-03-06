﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PengBo.Framwork.IRepository
{
    public interface IBaseRepository<T> : IDisposable where T : class,new()
    {
        DbContext Db { get; set; }
        void Tran_Begin(Action action);
        bool IsEmpty();
        bool Insert(T model);
        bool Delete(T model);
        bool Update(T model);
        Task<int> GetCountAsync();
        Task<bool> DeleteAsync(T model);

        Task<bool> DeleteAsync(IEnumerable<T> model);

        Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);

        Task BulkDelete(IEnumerable<T> model);

        Task<bool> InsertAsync(T model);

        Task<bool> InsertAsync(IEnumerable<T> model);

        Task BulkInsert(IEnumerable<T> model);

        Task<bool> UpdateAsync(T model);

        Task<bool> UpdateAsync(T model, params string[] param);

        Task BulkUpdate(IEnumerable<T> model);

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
