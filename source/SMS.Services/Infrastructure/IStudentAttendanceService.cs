using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using System;
using System.Linq.Expressions;
using DTOStudentAttendance = SMS.DTOs.DTOs.StudentAttendance;
using StudentAttendance = SMS.DATA.Models.StudentAttendance;

namespace SMS.Services.Infrastructure
{
    public interface IStudentAttendanceService
    {
        #region SMS Section
        ServiceResponse<DTOStudentAttendance> Get(string searchString, int pageNumber, int pageSize); StudentsAttendanceList Get(Guid? classId, Guid? schoolId, int pageNumber, int pageSize);
        StudentsAttendanceList Search(Expression<Func<StudentAttendance, bool>> predicate, int pageNumber, int pageSize);
        DTOStudentAttendance Get(Guid? id);
        StudentAttendanceResponse Create(DTOStudentAttendance dtoStudentAttendance);
        StudentAttendanceResponse Update(DTOStudentAttendance dtoStudentAttendance);
        void Delete(Guid? id, string deletedBy);

        #endregion

    }
}
