using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("StudentAttendanceDetail")]
    public partial class StudentAttendanceDetail : RequestBase
    {

        public Guid? AttendanceStatusId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? StudentAttendanceId { get; set; }
        public virtual StudentAttendance StudentAttendance { get; set; }
        public virtual AttendanceStatus AttendanceStatus { get; set; }
    }
}
