using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.DATA.Models
{
    [Table("StudentAttendance")]
    public partial class StudentAttendance : DomainBaseEnitity
    {
        public DateTime AttendanceDate { get; set; }
        public Guid? ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
