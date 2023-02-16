using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("StudentAttendance")]
    public partial class StudentAttendance : RequestBase
    {
        public DateTime AttendanceDate { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? ClassId { get; set; }

    }
}
