using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.Services.Infrastructure;
using LessonPlan = SMS.DATA.Models.LessonPlan;
using DTOLessonPlan = SMS.DTOs.DTOs.LessonPlan;
using SMS.DTOs.ReponseDTOs;
using System.Text.RegularExpressions;


namespace SMS.Services.Implementation
{

    public class LessonPlanService : ILessonPlanService
    {
        private readonly IRepository<LessonPlan> _repository;
        private readonly IMapper _mapper;
        public LessonPlanService(IRepository<LessonPlan> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region SMS Section
        public LessonPlansList Get(int pageNumber, int pageSize)
        {
            var lessonPlans = _repository.Get().Where(lp => lp.IsDeleted == false).OrderByDescending(lp => lp.CreatedDate).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var lessonPlanCount = _repository.Get().Where(st => st.IsDeleted == false).Count();
            var lessonPlanList = new List<DTOLessonPlan>();
            foreach (var lessonPlan in lessonPlans)
            {
                lessonPlanList.Add(_mapper.Map<LessonPlan, DTOLessonPlan>(lessonPlan));
            }
            var lessonPlansList = new LessonPlansList()
            {
                LessonPlans = lessonPlanList,
                LessonPlansCount = lessonPlanCount
            };
            return lessonPlansList;
        }
        public DTOLessonPlan Get(Guid? id)
        {
            if (id == null) return null;
            var lessonplanRecord = _repository.Get().FirstOrDefault(lp => lp.Id == id && lp.IsDeleted == false);
            var lessonplan = _mapper.Map<LessonPlan, DTOLessonPlan>(lessonplanRecord);
            return lessonplan;
        }
        public LessonPlanResponse Create(DTOLessonPlan dtoLessonplan)
        {
            var validationResult = Validation(dtoLessonplan);
            if (validationResult.IsError)
            {
                return validationResult;
            }
            dtoLessonplan.CreatedDate = DateTime.UtcNow;
            dtoLessonplan.IsDeleted = false;
            if (dtoLessonplan.Id == Guid.Empty)
            {
                dtoLessonplan.Id = Guid.NewGuid();
            }
            dtoLessonplan.SchoolId = dtoLessonplan.School?.Id;
            dtoLessonplan.School = null;
            _repository.Add(_mapper.Map<DTOLessonPlan, LessonPlan>(dtoLessonplan));
            return validationResult;
        }

        public LessonPlanResponse Update(DTOLessonPlan dtoLessonplan)
        {
            var validationResult = Validation(dtoLessonplan);
            if (validationResult.IsError)
            {
                return validationResult;
            }
            var lessonplan = Get(dtoLessonplan.Id);
            dtoLessonplan.UpdateDate = DateTime.UtcNow;
            dtoLessonplan.SchoolId = dtoLessonplan.School.Id;
            dtoLessonplan.School = null;
            var mergedLessonPlan = _mapper.Map(dtoLessonplan, lessonplan);
            _repository.Update(_mapper.Map<DTOLessonPlan, LessonPlan>(mergedLessonPlan));
            return validationResult;
        }

        public void Delete(Guid? id, string deletedBy)
        {
            if (id == null)
                return;
            var lessonplan = Get(id);
            lessonplan.DeletedBy = deletedBy;
            lessonplan.IsDeleted = true;
            lessonplan.DeletedDate = DateTime.UtcNow;
            _repository.Update(_mapper.Map<DTOLessonPlan, LessonPlan>(lessonplan));
        }
        private LessonPlanResponse Validation(DTOLessonPlan dtoLessonplan)
        {
            //var alphaRegex = new Regex("^[a-zA-Z ]+$");
            //var numericRegex = new Regex("^[0-9]*$");
            var alphanumericRegex = new Regex("^[a-zA-Z0-9 ]*$");
            if (dtoLessonplan == null)
            {
                return PrepareFailureResponse(dtoLessonplan.Id,
                    "Invalid",
                    "Object cannot be null"
                    );
            }
            if (string.IsNullOrWhiteSpace(dtoLessonplan.Name) || dtoLessonplan.Name.Length > 100)
            {
                return PrepareFailureResponse(dtoLessonplan.Id,
                    "InvalidName",
                    "Name may null or exceed than 100 characters"
                    );
            }
            if (!alphanumericRegex.IsMatch(dtoLessonplan.Name))
            {
                return PrepareFailureResponse(dtoLessonplan.Id,
                   "InvalidName",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (string.IsNullOrWhiteSpace(dtoLessonplan.Text))
            {
                return PrepareFailureResponse(dtoLessonplan.Id,
                    "InvalidText",
                    "This field cannot be null"
                    );
            }
            if (!alphanumericRegex.IsMatch(dtoLessonplan.Text))
            {
                return PrepareFailureResponse(dtoLessonplan.Id,
                   "InvalidName",
                   "Text Field doesn't contain any numbers"
                   );
            }
            if (dtoLessonplan.FromDate == null)
            {
                return PrepareFailureResponse(dtoLessonplan.Id,
                    "InvalidField",
                    "This field cannot be null"
                    );
            }
            if (dtoLessonplan.ToDate == null)
            {
                return PrepareFailureResponse(dtoLessonplan.Id,
                    "InvalidField",
                    "This field cannot be null"
                    );
            }
            return PrepareSuccessResponse(dtoLessonplan.Id,
                    "NoError",
                    "No Error Found"
                    );
        }
        private LessonPlanResponse PrepareFailureResponse(Guid id, string errorMessage, string descriptionMessage)
        {
            return new LessonPlanResponse
            {
                Id = id,
                IsError = true,
                StatusCode = "400",
                Message = errorMessage,
                Description = descriptionMessage
            };
        }
        private LessonPlanResponse PrepareSuccessResponse(Guid id, string message, string descriptionMessage)
        {
            return new LessonPlanResponse
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
