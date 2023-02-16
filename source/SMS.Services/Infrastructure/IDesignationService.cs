using SMS.DTOs.DTOs;
using System;
using DTODesignation = SMS.DTOs.DTOs.Designation;
namespace SMS.Services.Infrastructure
{
    public interface IDesignationService
    {
        #region SMS Section
        ServiceResponse<DTODesignation> Get(string searchString, int pageNumber, int pageSize);
        DTODesignation Get(Guid? id);
        Guid Create(DTODesignation dtoDesignation);
        void Update(DTODesignation dtoDesignation);
        void Delete(Guid? id);
        #endregion

    }
}
