using System;

namespace SMS.DATA.Models
{
    public class School : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public string DeletedBy { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
