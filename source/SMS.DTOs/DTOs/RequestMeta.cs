using System;

namespace SMS.DTOs.DTOs
{
    public class RequestMeta : DtoBaseEntity
    {
        public string ModuleName { get; set; }

        public string Type { get; set; }

        public Guid? ModuleId { get; set; }
        
    }
}
