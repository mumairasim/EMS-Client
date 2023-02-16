using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Designation = SMS.DATA.Models.Designation;
using DTODesignation = SMS.DTOs.DTOs.Designation;

namespace SMS.Services.Implementation
{
    public class DesignationService : IDesignationService
    {
        private readonly IRepository<Designation> _repository;
        private IMapper _mapper;
        public DesignationService(IRepository<Designation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #region SMS Section
        public ServiceResponse<DTODesignation> Get(string searchString, int pageNumber, int pageSize)
        {
            var resultSet = _repository.Get()
              .Where(cl => string.IsNullOrEmpty(searchString) || cl.Name.ToLower().Contains(searchString.ToLower()))
              .Where(cl => cl.IsDeleted == false).OrderByDescending(st => st.Id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var resultCount = resultSet.Count;
            var tempList = new List<DTODesignation>();
            foreach (var item in resultSet)
            {
                tempList.Add(_mapper.Map<Designation, DTODesignation>(item));
            }
            var finalList = new ServiceResponse<DTODesignation>()
            {
                Items = tempList,
                Count = resultCount
            };
            return finalList;
        }
        public DTODesignation Get(Guid? id)
        {
            if (id == null) return null;
            var designationRecord = _repository.Get().FirstOrDefault(d => d.Id == id && d.IsDeleted == false);
            if (designationRecord == null) return null;

            return _mapper.Map<Designation, DTODesignation>(designationRecord);
        }
        public Guid Create(DTODesignation dtoDesignation)
        {
            dtoDesignation.CreatedDate = DateTime.UtcNow;
            dtoDesignation.IsDeleted = false;
            if (dtoDesignation.Id == Guid.Empty)
            {
                dtoDesignation.Id = Guid.NewGuid();
            }
            _repository.Add(_mapper.Map<DTODesignation, Designation>(dtoDesignation));
            return dtoDesignation.Id;
        }
        public void Update(DTODesignation dtoDesignation)
        {
            var designation = Get(dtoDesignation.Id);
            dtoDesignation.UpdateDate = DateTime.UtcNow;
            var mergedDesignation = _mapper.Map(dtoDesignation, designation);
            _repository.Update(_mapper.Map<DTODesignation, Designation>(mergedDesignation));
        }
        public void Delete(Guid? id)
        {
            if (id == null)
                return;
            var designation = Get(id);
            designation.IsDeleted = true;
            designation.DeletedDate = DateTime.UtcNow;
            _repository.Update(_mapper.Map<DTODesignation, Designation>(designation));
        }
        #endregion
    }
}
