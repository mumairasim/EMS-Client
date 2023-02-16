using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.Services.Infrastructure;
using School = SMS.DATA.Models.School;
using DTOSchool = SMS.DTOs.DTOs.School;

namespace SMS.Services.Implementation
{
    public class SchoolService : ISchoolService
    {
        private readonly IRepository<School> _repository;
        private readonly IMapper _mapper;
        public SchoolService(IRepository<School> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region SMS Section
        public SchoolsList Get(int pageNumber, int pageSize)
        {
            var schools = _repository.Get().Where(cl => cl.IsDeleted == false).OrderByDescending(st => st.Id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var schoolCount = _repository.Get().Where(st => st.IsDeleted == false).Count();
            var schoolTempList = new List<DTOSchool>();
            foreach (var school in schools)
            {
                schoolTempList.Add(_mapper.Map<School, DTOSchool>(school));
            }
            var schoolsList = new SchoolsList()
            {
                Schools = schoolTempList,
                SchoolsCount = schoolCount
            };
            return schoolsList;
        }
        public SchoolsList Get(string searchString, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return Get(pageNumber, pageSize);
            var schools = _repository.Get().Where(cl =>
                (
                    cl.Name.Contains(searchString) ||
                    cl.Location.Contains(searchString)
                    ) &&
                cl.IsDeleted == false).OrderByDescending(st => st.Id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var schoolCount = _repository.Get().Count(st => (
                                                                st.Name.Contains(searchString) ||
                                                                st.Location.Contains(searchString)
                                                            ) &&
                                                            st.IsDeleted == false);
            var schoolTempList = new List<DTOSchool>();
            foreach (var school in schools)
            {
                schoolTempList.Add(_mapper.Map<School, DTOSchool>(school));
            }
            var schoolsList = new SchoolsList()
            {
                Schools = schoolTempList,
                SchoolsCount = schoolCount
            };
            return schoolsList;
        }
        public List<DTOSchool> GetAll()
        {
            var schools = _repository.Get().Where(cl => cl.IsDeleted == false).ToList();
            return _mapper.Map<List<School>, List<DTOSchool>>(schools);
        }
        public DTOSchool Get(Guid? id)
        {
            if (id == null) return null;
            var schoolRecord = _repository.Get().FirstOrDefault(s => s.Id == id);
            if (schoolRecord == null) return null;

            return _mapper.Map<School, DTOSchool>(schoolRecord);
        }
        public void Create(DTOSchool dtoSchool)
        {
            dtoSchool.CreatedDate = DateTime.UtcNow;
            dtoSchool.IsDeleted = false;
            if (dtoSchool.Id == Guid.Empty)
            {
                dtoSchool.Id = Guid.NewGuid();
            }
            _repository.Add(_mapper.Map<DTOSchool, School>(dtoSchool));
        }
        public void Update(DTOSchool dtoSchool)
        {
            var school = Get(dtoSchool.Id);
            dtoSchool.UpdateDate = DateTime.UtcNow;
            var mergedSchool = _mapper.Map(dtoSchool, school);
            _repository.Update(_mapper.Map<DTOSchool, School>(mergedSchool));
        }
        public void Delete(Guid? id, string DeletedBy)
        {
            if (id == null)
                return;
            var school = Get(id);
            school.IsDeleted = true;
            school.DeletedBy = DeletedBy;

            school.DeletedDate = DateTime.UtcNow;
            _repository.Update(_mapper.Map<DTOSchool, School>(school));
        }
        #endregion

    }
}
