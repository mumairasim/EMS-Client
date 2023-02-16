using System;

namespace SMS.DTOs.DTOs
{

    public class Assignment : DtoBaseEntity
    {

        public string AssignmentText { get; set; }

        public DateTime? LastDateOfSubmission { get; set; }

        public Guid? InstructorId { get; set; }

    }
}
