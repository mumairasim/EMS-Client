using SMS.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMS.DATA.Infrastructure
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetRaw();
        T Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void SoftDelete(T entity);
        void ExecuteSqlCommand(string sqlQuery);
        IQueryable<T> TableNoTracking { get; }
        IQueryable<T> Table { get; }

        //DbQuery<T> ViewDbQuery { get; }
    }
}
