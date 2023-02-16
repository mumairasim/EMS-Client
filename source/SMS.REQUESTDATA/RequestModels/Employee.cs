using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("Employee")]
    public partial class Employee : RequestBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? EmployeeNumber { get; set; }

        public Guid? PersonId { get; set; }

        public Guid? DesignationId { get; set; }
        public Guid? SchoolId { get; set; }
        public virtual Person Person { get; set; }
    }
}
