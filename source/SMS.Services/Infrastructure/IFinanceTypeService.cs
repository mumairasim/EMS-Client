using SMS.DTOs.DTOs;
using System;
using DTOFinanceType = SMS.DTOs.DTOs.FinanceType;

namespace SMS.Services.Infrastructure
{
    public interface IFinanceTypeService
    {
        #region SMS Section
        /// <summary>
        /// Service level call : Return all records of a FinanceType
        /// </summary>
        /// <returns></returns>
        ServiceResponse<DTOFinanceType> Get(string searchString, int pageNumber, int pageSize);

        /// <summary>
        /// Retruns a Single Record of a FinanceType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DTOFinanceType Get(Guid? id);

        /// <summary>
        /// Retruns a Single Record of a FinanceType by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DTOFinanceType GetByName(string name);

        /// <summary>
        /// Service level call : Creates a single record of a FinanceType
        /// </summary>
        /// <param name="dtoFinanceType"></param>
        void Create(DTOFinanceType dTOFinanceType);

        /// <summary>
        /// Service level call : Updates the Single Record of a FinanceType 
        /// </summary>
        /// <param name="dtoFinanceType"></param>
        void Update(DTOFinanceType dTOFinanceType);

        /// <summary>
        /// Service level call : Delete a single record of a FinanceType
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid? id, string deletedBy);
        #endregion

    }
}
