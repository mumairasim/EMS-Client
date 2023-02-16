using System;
using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.ReponseDTOs;
using SMS.Services.Infrastructure;
using Period = SMS.DATA.Models.Period;
using DTOPeriod = SMS.DTOs.DTOs.Period;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Services.Implementation
{
    public class PeriodService : IPeriodService
    {
        private readonly IRepository<Period> _repository;
        private readonly IMapper _mapper;
        public PeriodService(IRepository<Period> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #region SMS Section
        public GenericApiResponse Create(DTOPeriod dtoPeriod)
        {
            try
            {
                if (dtoPeriod.StartTime != null && dtoPeriod.EndTime != null && dtoPeriod.CourseId != null &&
                    dtoPeriod.TeacherId != null)
                {
                    dtoPeriod.CreatedDate = DateTime.Now;
                    dtoPeriod.IsDeleted = false;
                    if (dtoPeriod.Id == Guid.Empty)
                    {
                        dtoPeriod.Id = Guid.NewGuid();
                    }
                    HelpingMethodForRelationship(dtoPeriod);
                    _repository.Add(_mapper.Map<DTOPeriod, Period>(dtoPeriod));
                    return PrepareSuccessResponse("success", "");
                }

                return PrepareFailureResponse("Error", "Incomplete Data");
            }
            catch (Exception e)
            {
                return PrepareFailureResponse("error", e.Message);
            }

        }
        public List<DTOPeriod> View(Guid Id)
        {
            var periods = _repository.Get().Where(tt => tt.IsDeleted == false && tt.TimeTableDetailId == Id).OrderBy(x=>x.StartTime).ToList();
            var mappedPeriods = new List<DTOPeriod>();
            foreach (var item in periods)
            {
                mappedPeriods.Add(_mapper.Map<Period, DTOPeriod>(item));
            }
            return mappedPeriods;
        }
        private void HelpingMethodForRelationship(DTOPeriod dtoTimeTableDetail)
        {
            dtoTimeTableDetail.CourseId = dtoTimeTableDetail.Course.Id;
            dtoTimeTableDetail.Course = null;
            dtoTimeTableDetail.TeacherId = dtoTimeTableDetail.Employee.Id;
            dtoTimeTableDetail.Employee = null;
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
        #endregion

    }
}
