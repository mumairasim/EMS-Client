using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("Student")]
    public partial class Student : RequestBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegistrationNumber { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? ClassId { get; set; }
        public Guid? ImageId { get; set; }

        public Guid? SchoolId { get; set; }

        public virtual File Image { get; set; }
        public string PreviousSchoolName { get; set; }
        public string ReasonForLeaving { get; set; }

        public virtual Person Person { get; set; }

    }
}
