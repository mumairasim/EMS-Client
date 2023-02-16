using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{

    [Table("ClassAssignement")]
    public partial class ClassAssignement : RequestBase
    {

        public Guid? ClassId { get; set; }

        public Guid? AssignmentId { get; set; }

    }
}
