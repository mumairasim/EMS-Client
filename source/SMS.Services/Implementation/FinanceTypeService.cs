using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using DBFinanceType = SMS.DATA.Models.FinanceType;
using DTOFinanceType = SMS.DTOs.DTOs.FinanceType;
namespace SMS.Services.Implementation
{
    public class FinanceTypeService : IFinanceTypeService
    {
        #region Properties
        private readonly IRepository<DBFinanceType> _repository;
        private IMapper _mapper;
        #endregion

        #region Init

        public FinanceTypeService(IRepository<DBFinanceType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        #region SMS Section

        /// <summary>
        /// Service level call : Creates a single record of a FinanceType
        /// </summary>
        /// <param name="dTOFinanceType"></param>
        public void Create(DTOFinanceType dTOFinanceType)
        {
            dTOFinanceType.CreatedDate = DateTime.Now;
            dTOFinanceType.IsDeleted = false;

            if (dTOFinanceType.Id == Guid.Empty)
            {
                dTOFinanceType.Id = Guid.NewGuid();
            }

            _repository.Add(_mapper.Map<DTOFinanceType, DBFinanceType>(dTOFinanceType));
        }

        /// <summary>
        /// Service level call : Delete a single record of a FinanceType
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid? id, string deletedBy)
        {
            if (id == null)
                return;
            var financeType = Get(id);
            financeType.IsDeleted = true;
            financeType.DeletedBy = deletedBy;
            financeType.DeletedDate = DateTime.Now;
            _repository.Update(_mapper.Map<DTOFinanceType, DBFinanceType>(financeType));
        }

        /// <summary>
        /// Retruns a Single Record of a FinanceType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DTOFinanceType Get(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var financeType = _repository.Get().FirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
            var financeTypeDto = _mapper.Map<DBFinanceType, DTOFinanceType>(financeType);

            return financeTypeDto;
        }

        /// <summary>
        ///  Retruns a Single Record of a FinanceType by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DTOFinanceType GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var financeType = _repository.Get().FirstOrDefault(x => x.Type == name && (x.IsDeleted == false || x.IsDeleted == null));
            var financeTypeDto = _mapper.Map<DBFinanceType, DTOFinanceType>(financeType);

            return financeTypeDto;
        }

        /// <summary>
        /// Service level call : Updates the Single Record of a FinanceType 
        /// </summary>
        /// <param name="dtoFinanceType"></param>
        public void Update(DTOFinanceType dtoFinanceType)
        {
            var FinanceType = Get(dtoFinanceType.Id);
            if (FinanceType != null)
            {
                dtoFinanceType.UpdateDate = DateTime.Now;
                dtoFinanceType.IsDeleted = false;
                var updated = _mapper.Map(dtoFinanceType, FinanceType);
                var updatedDbRec = _mapper.Map<DTOFinanceType, DBFinanceType>(updated);
                _repository.Update(updatedDbRec);
            }
        }

        /// <summary>
        /// Service level call : Return all records of a FinanceType
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<DTOFinanceType> Get(string searchString, int pageNumber, int pageSize)
        {
            var resultSet = _repository.Get()
                .Where(cl => string.IsNullOrEmpty(searchString) || cl.Type.ToLower().Contains(searchString.ToLower()))
                .Where(cl => cl.IsDeleted == false).OrderByDescending(st => st.Id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var resultCount = resultSet.Count;
            var tempList = new List<DTOFinanceType>();
            foreach (var item in resultSet)
            {
                tempList.Add(_mapper.Map<DBFinanceType, DTOFinanceType>(item));
            }
            var finalList = new ServiceResponse<DTOFinanceType>()
            {
                Items = tempList,
                Count = resultCount
            };
            return finalList;
        }

        #endregion
    }
}
