using SMS.DTOs.DTOs;
using System;
using DTOStudentDiary = SMS.DTOs.DTOs.StudentDiary;

namespace SMS.Services.Infrastructure
{
    public interface IStudentDiaryService
    {
        #region SMS Section
        ServiceResponse<StudentDiary> Get(string searchString, int pageNumber, int pageSize);
        DTOStudentDiary Get(Guid? id);
        void Create(DTOStudentDiary StudentDiary);
        void Update(DTOStudentDiary dtoStudentDiary);
        void Delete(Guid? id, string DeletedBy);
        #endregion

    }
}
