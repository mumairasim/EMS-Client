using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.DATA.Models
{
    [Table("TimeTable")]
    public partial class TimeTable : DomainBaseEnitity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeTable()
        {
        }

        [StringLength(500)]
        public string TimeTableName { get; set; }
        public Guid? ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
