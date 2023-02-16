using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{

    [Table("Class")]
    public partial class Class : RequestBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
        }

        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }
        public Guid? SchoolId { get; set; }
    }
}
