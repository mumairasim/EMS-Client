using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DTOTeacherDiary = SMS.DTOs.DTOs.TeacherDiary;
using TeacherDiary = SMS.DATA.Models.TeacherDiary;

namespace SMS.Services.Implementation
{
    public class TeacherDiaryService : ITeacherDiaryService
    {
        private readonly IRepository<TeacherDiary> _repository;
        private readonly IMapper _mapper;
        public TeacherDiaryService(IRepository<TeacherDiary> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #region SMS Section
        public ServiceResponse<DTOTeacherDiary> Get(int pageNumber, int pageSize, string searchString)
        {
            var resultSet = _repository.Get()
                .Where(cl => string.IsNullOrEmpty(searchString) || cl.Name.ToLower().Contains(searchString.ToLower()))
                .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.School.Name.ToLower().Contains(searchString.ToLower())))
                .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.DairyText.ToLower().Contains(searchString.ToLower())))
                .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.Employee.Person.FirstName.ToLower().Contains(searchString.ToLower())))
                .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.Employee.Person.LastName.ToLower().Contains(searchString.ToLower())))
                .Where(cl => cl.IsDeleted == false).OrderByDescending(st => st.Id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var courseCount = resultSet.Count;
            var tempList = new List<DTOTeacherDiary>();
            foreach (var item in resultSet)
            {
                tempList.Add(_mapper.Map<TeacherDiary, DTOTeacherDiary>(item));
            }
            var finalList = new ServiceResponse<DTOTeacherDiary>()
            {
                Items = tempList,
                Count = courseCount
            };
            return finalList;
        }

        public DTOTeacherDiary Get(Guid? id)
        {
            if (id == null) return null;
            var teacherDiaryRecord = _repository.Get().FirstOrDefault(td => td.Id == id && td.IsDeleted == false);
            var teacherDiary = _mapper.Map<TeacherDiary, DTOTeacherDiary>(teacherDiaryRecord);
            return teacherDiary;
        }

        public TeacherDiaryResponse Create(DTOTeacherDiary dtoteacherDiary)
        {
            var validationResult = Validation(dtoteacherDiary);
            if (validationResult.IsError)
            {
                return validationResult;
            }
            dtoteacherDiary.CreatedDate = DateTime.Now;
            dtoteacherDiary.IsDeleted = false;
            if (dtoteacherDiary.Id == Guid.Empty)
            {
                dtoteacherDiary.Id = Guid.NewGuid();
            }
            HelpingMethodForRelationship(dtoteacherDiary);
            _repository.Add(_mapper.Map<DTOTeacherDiary, TeacherDiary>(dtoteacherDiary));
            return validationResult;
        }
        public TeacherDiaryResponse Update(DTOTeacherDiary dtoteacherDiary)
        {
            var validationResult = Validation(dtoteacherDiary);
            if (validationResult.IsError)
            {
                return validationResult;
            }
            var teacherDiary = Get(dtoteacherDiary.Id);
            dtoteacherDiary.UpdateDate = DateTime.UtcNow;
            HelpingMethodForRelationship(dtoteacherDiary);
            var mergedTeacherDiary = _mapper.Map(dtoteacherDiary, teacherDiary);
            _repository.Update(_mapper.Map<DTOTeacherDiary, TeacherDiary>(mergedTeacherDiary));
            return validationResult;
        }
        public void Delete(Guid? id, string DeletedBy)
        {
            if (id == null)
                return;
            var teacherDiary = Get(id);
            teacherDiary.IsDeleted = true;
            teacherDiary.DeletedBy = DeletedBy;
            teacherDiary.DeletedDate = DateTime.UtcNow;
            _repository.Update(_mapper.Map<DTOTeacherDiary, TeacherDiary>(teacherDiary));
        }
        private void HelpingMethodForRelationship(DTOTeacherDiary dtoteacherDiary)
        {
            dtoteacherDiary.SchoolId = dtoteacherDiary.School.Id;
            dtoteacherDiary.School = null;
            dtoteacherDiary.InstructorId = dtoteacherDiary.Employee.Id;
            dtoteacherDiary.Employee = null;
        }

        private TeacherDiaryResponse Validation(DTOTeacherDiary dtoteacherDiary)
        {
            var alphanumericRegex = new Regex("^[a-zA-Z0-9 ]*$");
            if (dtoteacherDiary == null)
            {
                return PrepareFailureResponse(dtoteacherDiary.Id,
                    "Invalid",
                    "Object cannot be null"
                    );
            }
            if (string.IsNullOrWhiteSpace(dtoteacherDiary.Name) || dtoteacherDiary.Name.Length > 100)
            {
                return PrepareFailureResponse(dtoteacherDiary.Id,
                    "InvalidName",
                    "Name may null or exceed than 100 characters"
                    );
            }
            if (!alphanumericRegex.IsMatch(dtoteacherDiary.Name))
            {
                return PrepareFailureResponse(dtoteacherDiary.Id,
                   "InvalidName",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (string.IsNullOrWhiteSpace(dtoteacherDiary.DairyText))
            {
                return PrepareFailureResponse(dtoteacherDiary.Id,
                    "InvalidText",
                    "This field cannot be null"
                    );
            }
            if (!alphanumericRegex.IsMatch(dtoteacherDiary.DairyText))
            {
                return PrepareFailureResponse(dtoteacherDiary.Id,
                   "InvalidName",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (dtoteacherDiary.DairyDate == null)
            {
                return PrepareFailureResponse(dtoteacherDiary.Id,
                    "InvalidField",
                    "This field cannot be null"
                    );
            }
            if (dtoteacherDiary.School == null)
            {
                return PrepareFailureResponse(dtoteacherDiary.Id,
                    "InvalidField",
                    "This field cannot be null"
                    );
            }
            if (dtoteacherDiary.Employee == null)
            {
                return PrepareFailureResponse(dtoteacherDiary.Id,
                    "InvalidField",
                    "This field cannot be null"
                    );
            }
            return PrepareSuccessResponse(dtoteacherDiary.Id,
                    "NoError",
                    "No Error Found"
                    );
        }
        private TeacherDiaryResponse PrepareFailureResponse(Guid id, string errorMessage, string descriptionMessage)
        {
            return new TeacherDiaryResponse
            {
                Id = id,
                IsError = true,
                StatusCode = "400",
                Message = errorMessage,
                Description = descriptionMessage
            };
        }
        private TeacherDiaryResponse PrepareSuccessResponse(Guid id, string message, string descriptionMessage)
        {
            return new TeacherDiaryResponse
            {
                Id = id,
                IsError = false,
                StatusCode = "200",
                Message = message,
                Description = descriptionMessage
            };
        }


        #endregion
    }
}
