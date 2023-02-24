using System;
using SMS.DTOs.DTOs;
using DTOSchool = SMS.DTOs.DTOs.School;
using System.Collections.Generic;
namespace SMS.Services.Infrastructure
{
    public interface ISchoolService
    {
        #region SMS
        SchoolsList Get(int pageNumber, int pageSize);
        SchoolsList Get(string searchString, int pageNumber, int pageSize);
        List<DTOSchool> GetAll();
        DTOSchool Get(Guid? id);
        void Create(DTOSchool dtoSchool);
        void Update(DTOSchool dtoSchool);
        void Delete(Guid? id, string DeletedBy);
        bool Register(DTOSchool dtoSchool);
        bool IsAlreadyRegistered();
        #endregion

    }
}
