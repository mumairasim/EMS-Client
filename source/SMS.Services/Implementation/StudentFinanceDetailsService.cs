using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using DBStudentFinanceDetails = SMS.DATA.Models.StudentFinanceDetail;
using DTOStudentFinanceDetails = SMS.DTOs.DTOs.StudentFinanceDetail;

namespace SMS.Services.Implementation
{
    public class StudentFinanceDetailsService : IStudentFinanceDetailsService
    {
        #region Properties
        private readonly IRepository<DBStudentFinanceDetails> _repository;
        private readonly IFinanceTypeService _financeTypeService;
        private IMapper _mapper;
        #endregion

        #region Init

        public StudentFinanceDetailsService(IRepository<DBStudentFinanceDetails> repository, IFinanceTypeService financeTypeService, IMapper mapper)
        {
            _repository = repository;
            _financeTypeService = financeTypeService;
            _mapper = mapper;
        }

        #endregion

        #region Service Calls

        /// <summary>
        /// Service level call : Creates a single record of a StudentFinances
        /// </summary>
        /// <param name="dTOStudentFinanceDetails"></param>
        public void Create(DTOStudentFinanceDetails dTOStudentFinanceDetails)
        {
            dTOStudentFinanceDetails.CreatedDate = DateTime.UtcNow;
            dTOStudentFinanceDetails.IsDeleted = false;
            if (dTOStudentFinanceDetails.Id == Guid.Empty)
            {
                dTOStudentFinanceDetails.Id = Guid.NewGuid();
            }

            _repository.Add(_mapper.Map<DTOStudentFinanceDetails, DBStudentFinanceDetails>(dTOStudentFinanceDetails));
        }

        /// <summary>
        /// Service level call : Delete a single record of a StudentFinances
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid? id)
        {
            if (id == null)
                return;
            var StudentFinances = Get(id);
            if (StudentFinances != null)
            {
                StudentFinances.IsDeleted = true;
                StudentFinances.DeletedDate = DateTime.UtcNow;
                _repository.Update(_mapper.Map<DTOStudentFinanceDetails, DBStudentFinanceDetails>(StudentFinances));
            }

        }

        /// <summary>
        /// Retruns a Single Record of a StudentFinances
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DTOStudentFinanceDetails Get(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var StudentFinances = _repository.Get().FirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
            var StudentFinancesDto = _mapper.Map<DBStudentFinanceDetails, DTOStudentFinanceDetails>(StudentFinances);

            return StudentFinancesDto;
        }

        public DTOStudentFinanceDetails GetByFeeType(Guid? studentId, string feeType)
        {
            if (studentId == null || string.IsNullOrEmpty(feeType))
            {
                return null;
            }
            var financeType = _financeTypeService.GetByName(feeType);
            var StudentFinances = _repository.Get().FirstOrDefault(x => x.StudentId == studentId && x.FinanceTypeId == financeType.Id && (x.IsDeleted == false || x.IsDeleted == null));
            var StudentFinancesDto = _mapper.Map<DBStudentFinanceDetails, DTOStudentFinanceDetails>(StudentFinances);

            return StudentFinancesDto;
        }

        /// <summary>
        /// Returns a list of Records of a StudentFinances by student Id
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public List<DTOStudentFinanceDetails> GetByStudentId(Guid? studentId)
        {
            if (studentId == null)
            {
                return null;
            }

            var StudentFinances = _repository.Get().Where(x => x.StudentId == studentId && (x.IsDeleted == false || x.IsDeleted == null)).ToList();
            var StudentFinancesDto = _mapper.Map<List<DBStudentFinanceDetails>, List<DTOStudentFinanceDetails>>(StudentFinances);

            return StudentFinancesDto;
        }

        /// <summary>
        /// Service level call : Updates the Single Record of a StudentFinances 
        /// </summary>
        /// <param name="dTOStudentFinanceDetails"></param>
        public void Update(DTOStudentFinanceDetails dTOStudentFinanceDetails)
        {
            var StudentFinances = Get(dTOStudentFinanceDetails.Id);
            if (StudentFinances != null)
            {
                dTOStudentFinanceDetails.UpdateDate = DateTime.UtcNow;
                dTOStudentFinanceDetails.IsDeleted = false;
                var updated = _mapper.Map(dTOStudentFinanceDetails, StudentFinances);

                _repository.Update(_mapper.Map<DTOStudentFinanceDetails, DBStudentFinanceDetails>(updated));
            }
        }

        /// <summary>
        /// Service level call : Return all records of a StudentFinances
        /// </summary>
        /// <returns></returns>
        List<DTOStudentFinanceDetails> IStudentFinanceDetailsService.GetAll()
        {
            var StudentFinancess = _repository.Get().Where(x => (x.IsDeleted == false || x.IsDeleted == null)).ToList();
            var StudentFinancesList = new List<DTOStudentFinanceDetails>();
            foreach (var StudentFinances in StudentFinancess)
            {
                StudentFinancesList.Add(_mapper.Map<DBStudentFinanceDetails, DTOStudentFinanceDetails>(StudentFinances));
            }
            return StudentFinancesList;
        }

        #endregion

    }
}
