using SMS.DATA.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.DATA.Models
{
    [Table("Person")]
    public partial class Person : DomainBaseEnitity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            Employees = new HashSet<Employee>();
            Students = new HashSet<Student>();
        }
        public Guid? AspNetUserId { get; set; }
        public int? Age { get; set; }
        [StringLength(250)]
        public string FirstName { get; set; }

        [StringLength(250)]
        public string LastName { get; set; }
        public string ParentName { get; set; }

        [StringLength(50)]
        public string Cnic { get; set; }
        public string ParentCnic { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DOB { get; set; }

        [StringLength(250)]
        public string Nationality { get; set; }

        [StringLength(250)]
        public string Religion { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string ParentOccupation { get; set; }
        public string ParentRelation { get; set; }
        public string ParentHighestEducation { get; set; }
        public string ParentNationality { get; set; }
        public string ParentEmail { get; set; }
        public string ParentOfficeAddress { get; set; }
        public string ParentCity { get; set; }
        public string City { get; set; }
        public string ParentMobile1 { get; set; }
        public string ParentMobile2 { get; set; }
        public string ParentEmergencyName { get; set; }
        public string ParentEmergencyRelation { get; set; }
        public string ParentEmergencyMobile { get; set; }
        public Guid? ImageId { get; set; }
        public virtual File Image { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
