using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.DATA.Models
{

    [Table("ClassAssignement")]
    public partial class ClassAssignement : DomainBaseEnitity
    {

        public Guid? ClassId { get; set; }

        public Guid? AssignmentId { get; set; }

        public virtual Assignment Assignment { get; set; }

        public virtual Class Class { get; set; }
    }
}
