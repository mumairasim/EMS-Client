using SMS.DATA.Models.Enums;

namespace SMS.DATA
{
    /// <summary>
    /// Use this when there is no need of extra properties
    /// like AspNetUser table
    /// </summary>
    public class BaseEntity
    {
        public RequestStatus? ApprovalStatus { get; set; }
    }
}
