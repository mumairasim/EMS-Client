using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("AttendanceStatus")]
    public partial class AttendanceStatus : RequestBase
    {
        public string Status { get; set; }
    }
}
