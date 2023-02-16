using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.DATA.Models
{
    [Table("TeacherDiary")]
    public partial class TeacherDiary : DomainBaseEnitity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TeacherDiary()
        {
            ClassTeacherDiaries = new HashSet<ClassTeacherDiary>();
        }
        public string DairyText { get; set; }
        public string Name { get; set; }

        public DateTime? DairyDate { get; set; }

        public Guid? InstructorId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClassTeacherDiary> ClassTeacherDiaries { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
