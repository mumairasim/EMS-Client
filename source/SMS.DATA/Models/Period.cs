using System;

namespace SMS.DATA.Models
{
    public partial class Period : DomainBaseEnitity
    {
        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }
        public Guid? TeacherId { get; set; }

        public Guid? TimeTableDetailId { get; set; }

        public Guid? CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Employee Employee { get; set; }


        public virtual TimeTableDetail TimeTableDetail { get; set; }
    }
}
