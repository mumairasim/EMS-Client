using System;

namespace SMS.DTOs.DTOs
{
    public class CommonRequestModel : DtoBaseEntity
    {
        public RequestType RequestType { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string RequestFor { get; set; }
    }
}
