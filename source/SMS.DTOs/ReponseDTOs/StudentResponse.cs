using System;

namespace SMS.DTOs.ReponseDTOs
{
    public class StudentResponse : GenericApiResponse
    {
        public Guid? Id { get; set; }
        public bool IsError { get; set; }
    }
}
