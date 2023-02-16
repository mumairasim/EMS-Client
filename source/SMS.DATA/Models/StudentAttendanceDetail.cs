using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.DATA.Models
{
    [Table("StudentAttendanceDetail")]
    public partial class StudentAttendanceDetail : DomainBaseEnitity
    {

        public Guid? AttendanceStatusId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? StudentAttendanceId { get; set; }
        public virtual StudentAttendance StudentAttendance { get; set; }
        public virtual AttendanceStatus AttendanceStatus { get; set; }
        public virtual Student Student { get; set; }
    }
}
