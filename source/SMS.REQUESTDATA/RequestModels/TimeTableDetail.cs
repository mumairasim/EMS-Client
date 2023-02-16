using System;
using System.ComponentModel.DataAnnotations;

namespace SMS.REQUESTDATA.RequestModels
{
    public partial class TimeTableDetail : RequestBase
    {
        [StringLength(50)]
        public string Day { get; set; }

        public Guid? ClassId { get; set; }

        public Guid? PeriodId { get; set; }

        public Guid? TeacherId { get; set; }

        public Guid? TimeTableId { get; set; }

        public virtual Period Period { get; set; }

        public virtual TimeTable TimeTable { get; set; }
    }
}
