

namespace SMS.REQUESTDATA.Infrastructure
{
    public interface IRequestUnitOfWork
    {
        IRequestDbContext Context { get; }

        void Commit();
    }
}
