using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMS.REQUESTDATA.Infrastructure
{
    public interface IRequestRepository<T>
    {
        IQueryable<T> Get();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
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
