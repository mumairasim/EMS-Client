using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("StudentAssignment")]
    public partial class StudentAssignment : RequestBase
    {
        public Guid? StudentId { get; set; }

        public Guid? AssignmentId { get; set; }

    }
}
