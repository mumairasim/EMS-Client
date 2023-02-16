using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.DATA.Models
{
    [Table("Employee")]
    public partial class Employee : DomainBaseEnitity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Assignments = new HashSet<Assignment>();
            EmployeeFinanceDetails = new HashSet<EmployeeFinanceDetail>();
            StudentDiaries = new HashSet<StudentDiary>();
            TeacherDiaries = new HashSet<TeacherDiary>();
            Periods = new HashSet<Period>();
            Worksheets = new HashSet<Worksheet>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? EmployeeNumber { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? DesignationId { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual Designation Designation { get; set; }

        public virtual Person Person { get; set; }

        //public int SerialNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeFinanceDetail> EmployeeFinanceDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentDiary> StudentDiaries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeacherDiary> TeacherDiaries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Period> Periods { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Worksheet> Worksheets { get; set; }
    }
}
