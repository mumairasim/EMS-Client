using SMS.DATA.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.DATA.Models
{
    [Table("RequestMeta")]
    public partial class RequestMeta : DomainBaseEnitity
    {
        public Module ModuleName { get; set; }

        public RequestType Type { get; set; }

        public Guid? ModuleId { get; set; }
    }
}
