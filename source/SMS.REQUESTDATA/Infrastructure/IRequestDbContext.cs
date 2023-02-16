using System.Data.Entity;

namespace SMS.REQUESTDATA.Infrastructure
{
    public partial interface IRequestDbContext
    {
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }
}
