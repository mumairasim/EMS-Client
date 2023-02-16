using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("TeacherDiary")]
    public partial class TeacherDiary : RequestBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TeacherDiary()
        {
        }
        public string DairyText { get; set; }
        public string Name { get; set; }

        public DateTime? DairyDate { get; set; }

        public Guid? InstructorId { get; set; }
        public Guid? SchoolId { get; set; }
    }
}
