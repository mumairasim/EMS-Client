using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DATA.Models.Enums;
using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Class = SMS.DATA.Models.Class;
using DTOClass = SMS.DTOs.DTOs.Class;
using RequestMeta = SMS.DATA.Models.RequestMeta;

namespace SMS.Services.Implementation
{
    public class ClassService : IClassService
    {
        private readonly IRepository<Class> _repository;
        public IRequestMetaService _requestMetaService;
        private const string error_not_found = "Record not found";
        private const string server_error = "Server error";

        private readonly IMapper _mapper;
        public ClassService(IRepository<Class> repository, IMapper mapper, IRequestMetaService requestMetaService)
        {
            _repository = repository;
            _mapper = mapper;
            _requestMetaService = requestMetaService;
        }

        #region SMS Section
        public GenericApiResponse Create(DTOClass dtoClass)
        {
            try
            {
                dtoClass.CreatedDate = DateTime.UtcNow;
                dtoClass.IsDeleted = false;
                if (dtoClass.Id == Guid.Empty)
                {
                    dtoClass.Id = Guid.NewGuid();
                }
                HelpingMethodForRelationship(dtoClass);
                var dbClass = _mapper.Map<DTOClass, Class>(dtoClass);
                dbClass.ApprovalStatus = DATA.Models.Enums.RequestStatus.Pending;
                _repository.Add(dbClass);
                return PrepareSuccessResponse("Created", "Instance Created Successfully");

            }
            catch (Exception)
            {
                return PrepareFailureResponse("Error", server_error);
            }

        }
        public ClassesList Get(int pageNumber, int pageSize, string searchString = "")
        {
            var clasess = _repository.Get()
                .Where(cl => string.IsNullOrEmpty(searchString) || cl.School.Name.ToLower().Contains(searchString.ToLower()))
                .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.ClassName.ToLower().Contains(searchString.ToLower())))
                .Where(cl => cl.IsDeleted == false).OrderByDescending(st => st.Id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var ClassCount = clasess.Count();
            var classTempList = new List<DTOClass>();
            foreach (var Classes in clasess)
            {
                classTempList.Add(_mapper.Map<Class, DTOClass>(Classes));
            }
            var classesList = new ClassesList()
            {
                Classes = classTempList,
                classesCount = ClassCount
            };
            return classesList;
        }
        public DTOClass Get(Guid? id)
        {
            if (id == null) return null;
            var classRecord = _repository.Get().FirstOrDefault(cl => cl.Id == id && cl.IsDeleted == false);
            var classes = _mapper.Map<Class, DTOClass>(classRecord);

            return classes;
        }
        public List<DTOClass> GetBySchool(Guid? schoolId)
        {
            var classes = _repository.Get().Where(cl => cl.SchoolId == schoolId && cl.IsDeleted == false).ToList();
            var classList = new List<DTOClass>();
            foreach (var itemClass in classes)
            {
                classList.Add(_mapper.Map<Class, DTOClass>(itemClass));
            }
            return classList;
        }

        public GenericApiResponse Update(DTOClass dtoClass)
        {
            try
            {
                var Classes = Get(dtoClass.Id);
                if (Classes != null)
                {
                    dtoClass.UpdateDate = DateTime.UtcNow;
                    var mergedClass = _mapper.Map(dtoClass, Classes);
                    _repository.Update(_mapper.Map<DTOClass, Class>(mergedClass));
                    if (dtoClass.IsClient == true)
                    {
                        _requestMetaService.Create(new RequestMeta
                        {
                            ModuleId = dtoClass.Id,
                            SchoolId = dtoClass.SchoolId,
                            ModuleName = Module.Class,
                            ApprovalStatus = DATA.Models.Enums.RequestStatus.Pending,
                            Type = DATA.Models.Enums.RequestType.Update
                        });
                    }
                    return PrepareSuccessResponse("Updated", "Instance Updated Successfully");
                }
                return PrepareFailureResponse("Error", error_not_found);
            }
            catch (Exception)
            {
                return PrepareFailureResponse("Error", server_error);
            }

        }
        public void Delete(Guid? id, string DeletedBy)
        {
            if (id == null)
                return;
            var classes = Get(id);
            classes.IsDeleted = true;
            classes.DeletedDate = DateTime.UtcNow;
            classes.DeletedBy = DeletedBy;
            _repository.Update(_mapper.Map<DTOClass, Class>(classes));
        }
        #endregion
        private void HelpingMethodForRelationship(DTOClass dtoClass)
        {
            dtoClass.SchoolId = dtoClass.School.Id;
            dtoClass.School = null;
        }
        private GenericApiResponse PrepareFailureResponse(string errorMessage, string descriptionMessage)
        {
            return new GenericApiResponse
            {
                StatusCode = "400",
                Message = errorMessage,
                Description = descriptionMessage
            };
        }
        private GenericApiResponse PrepareSuccessResponse(string message, string descriptionMessage)
        {
            return new GenericApiResponse
            {
                StatusCode = "200",
                Message = message,
                Description = descriptionMessage
            };
        }
    }
}




