using System;

namespace SMS.DTOs.DTOs
{
    public class DtoBaseEntity
    {
        private string approvalStatus;

        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string DeletedBy { get; set; }

        public bool? IsDeleted { get; set; }
        public bool IsClient { get; set; } = false;
        public Guid? SchoolId { get; set; }
        public School School { get; set; }
        public string ApprovalStatus { get; set; }

    }
}
