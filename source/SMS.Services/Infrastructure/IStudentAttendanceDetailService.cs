using System;
using System.Collections.Generic;
using DTOStudentAttendanceDetail = SMS.DTOs.DTOs.StudentAttendanceDetail;
namespace SMS.Services.Infrastructure
{
    public interface IStudentAttendanceDetailService
    {
        #region SMS section
        DTOStudentAttendanceDetail Get(Guid? id);
        Guid Create(DTOStudentAttendanceDetail dtoStudentAttendance);
        void Create(List<DTOStudentAttendanceDetail> dtoStudentAttendanceDetailList, string createdBy, Guid id);
        void Update(DTOStudentAttendanceDetail dtoStudentAttendance);
        void Delete(Guid? id, string deletedBy);
        List<DTOStudentAttendanceDetail> GetByStudentAttendanceId(Guid? studentId);
        #endregion

    }
}
