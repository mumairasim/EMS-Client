using System;
using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using DTOStudent = SMS.DTOs.DTOs.Student;

namespace SMS.Services.Infrastructure
{
    public interface IStudentService
    {
        #region SMS Section
        StudentsList Get(int pageNumber, int pageSize);
        StudentsList Get(string searchString, int pageNumber, int pageSize);
        StudentsList Get(int registrationNumber, int pageNumber, int pageSize);
        DTOStudent Get(Guid? id);
        StudentsList Get(Guid classId, Guid schoolId);
        StudentResponse Create(DTOStudent student);
        StudentResponse Update(DTOStudent dtoStudent);
        void Delete(Guid? id, string DeletedBy);
        #endregion
    }
}
