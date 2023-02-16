using System;

namespace SMS.DTOs.DTOs
{
    public class StudentAttendanceDetail : DtoBaseEntity
    {
        public Guid? AttendanceStatusId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? StudentAttendanceId { get; set; }
        public StudentAttendance StudentAttendance { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
        public Student Student { get; set; }
    }
}
