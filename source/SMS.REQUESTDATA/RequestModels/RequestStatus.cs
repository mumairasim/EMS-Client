using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.REQUESTDATA.RequestModels
{
    [Table("RequestStatuses")]
    public class RequestStatus : BaseEntity
    {

        public string Type { get; set; }



    }
}
