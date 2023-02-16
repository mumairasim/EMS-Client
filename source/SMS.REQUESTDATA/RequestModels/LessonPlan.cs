using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("LessonPlan")]
    public partial class LessonPlan : RequestBase
    {
        public string Text { get; set; }

        public string Name { get; set; }
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
        public Guid? SchoolId { get; set; }
    }
}
