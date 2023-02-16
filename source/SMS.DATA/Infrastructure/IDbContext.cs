using System.Data.Entity;

namespace SMS.DATA.Infrastructure
{
    public partial interface IDbContext
    {
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);
        
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }
}
