using SMS.REQUESTDATA.Infrastructure;
using SMSRequestContext = SMS.REQUESTDATA.RequestModels.SMSRequest;

namespace SMS.REQUESTDATA.Implementation
{
    public class RequestUnitOfWork : IRequestUnitOfWork
    {
        public IRequestDbContext Context { get; }

        public RequestUnitOfWork()
        {
            Context = new SMSRequestContext();
        }
        public void Commit()
        {
            Context.SaveChanges();
        }



    }
}
