using System;

namespace SMS.DTOs.DTOs
{
    public class Period : DtoBaseEntity
    {
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? TimeTableDetailId { get; set; }
        public Guid? CourseId { get; set; }
        public Course Course { get; set; }
        public Employee Employee { get; set; }
        public TimeTableDetail TimeTableDetail { get; set; }
    }
}
