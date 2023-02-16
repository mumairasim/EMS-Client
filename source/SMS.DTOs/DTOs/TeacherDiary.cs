using System;

namespace SMS.DTOs.DTOs
{
    public class TeacherDiary : DtoBaseEntity
    {
        public string Name { get; set; }
        public string DairyText { get; set; }
        public DateTime? DairyDate { get; set; }
        public Guid? InstructorId { get; set; }
        public  Employee Employee { get; set; }
    }
}
