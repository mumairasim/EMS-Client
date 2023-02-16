using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.DATA.Models
{
    [Table("AttendanceStatus")]
    public partial class AttendanceStatus : DomainBaseEnitity
    {
        public string Status { get; set; }
    }
}
