using System;
using System.Collections.Generic;
using DTOStudentFinanceDetails = SMS.DTOs.DTOs.StudentFinanceDetail;

namespace SMS.Services.Infrastructure
{
    public interface IStudentFinanceDetailsService
    {
        #region SMS 
        /// <summary>
        /// Service level call : Return all records of a StudentFinanceDetails
        /// </summary>
        /// <returns></returns>
        List<DTOStudentFinanceDetails> GetAll();

        /// <summary>
        /// Retruns a Single Record of a StudentFinanceDetails
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DTOStudentFinanceDetails Get(Guid? id);

        /// <summary>
        /// Retruns List of Record of a StudentFinanceDetails by Student Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        List<DTOStudentFinanceDetails> GetByStudentId(Guid? studentId);

        /// <summary>
        /// Retruns List of Record of a StudentFinanceDetails by Fee type and student Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        DTOStudentFinanceDetails GetByFeeType(Guid? studentId, string feeType);

        /// <summary>
        /// Service level call : Creates a single record of a StudentFinanceDetails
        /// </summary>
        /// <param name="dtoStudentFinanceDetails"></param>
        void Create(DTOStudentFinanceDetails dTOStudentFinanceDetails);

        /// <summary>
        /// Service level call : Updates the Single Record of a StudentFinanceDetails 
        /// </summary>
        /// <param name="dtoStudentFinanceDetails"></param>
        void Update(DTOStudentFinanceDetails dTOStudentFinanceDetails);

        /// <summary>
        /// Service level call : Delete a single record of a StudentFinanceDetails
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid? id);
        #endregion

    }

}
