using System;

namespace SMS.REQUESTDATA
{
    public class RequestBase : BaseEntity
    {
        public Guid? RequestStatusId { get; set; }
        public Guid? RequestTypeId { get; set; }
    }
}
