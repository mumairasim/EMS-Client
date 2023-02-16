using System;

namespace SMS.DTOs.ReponseDTOs
{
    public class TeacherDiaryResponse : GenericApiResponse
    {
        public Guid? Id { get; set; }
        public bool IsError { get; set; }
    }
}
