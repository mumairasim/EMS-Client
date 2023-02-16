using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("TimeTable")]
    public partial class TimeTable : RequestBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeTable()
        {
        }

        [StringLength(500)]
        public string TimeTableName { get; set; }
        public Guid? SchoolId { get; set; }
    }
}
