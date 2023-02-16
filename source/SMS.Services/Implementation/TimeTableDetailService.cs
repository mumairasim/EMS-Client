using System;
using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.ReponseDTOs;
using SMS.Services.Infrastructure;
using TimeTableDetail = SMS.DATA.Models.TimeTableDetail;
using DTOTimeTableDetail = SMS.DTOs.DTOs.TimeTableDetail;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Services.Implementation
{
    public class TimeTableDetailService : ITimeTableDetailService
    {
        private readonly IRepository<TimeTableDetail> _repository;
        private readonly IMapper _mapper;
        private readonly IPeriodService _periodService;
        public TimeTableDetailService(IRepository<TimeTableDetail> repository, IMapper mapper, IPeriodService periodService)
        {
            _repository = repository;
            _mapper = mapper;
            _periodService = periodService;
        }
        #region SMS Section
        public GenericApiResponse Create(DTOTimeTableDetail dtoTimeTableDetail)
        {
            try
            {
                dtoTimeTableDetail.CreatedDate = DateTime.Now;
                dtoTimeTableDetail.IsDeleted = false;

                if (dtoTimeTableDetail.Id == Guid.Empty)
                {
                    dtoTimeTableDetail.Id = Guid.NewGuid();
                }

                var timeTableDetail = _repository.Add(_mapper.Map<DTOTimeTableDetail, TimeTableDetail>(dtoTimeTableDetail));
                if (dtoTimeTableDetail.Periods != null)
                    foreach (var period in dtoTimeTableDetail.Periods)
                    {
                        period.TimeTableDetailId = timeTableDetail.Id;
                        _periodService.Create(period);
                    }

                return PrepareSuccessResponse("success", "");
            }
            catch (Exception e)
            {
                return PrepareFailureResponse("error", e.Message);
            }
        }
        public List<DTOTimeTableDetail> View(Guid Id)
        {
            var timeTableDetails = _repository.Get().Where(tt => tt.IsDeleted == false && tt.TimeTableId == Id).ToList();
            var mappedTimeTableDetailsList = new List<DTOTimeTableDetail>();
            foreach (var item in timeTableDetails)
            {
                var mappedTimeTableDetail = _mapper.Map<TimeTableDetail, DTOTimeTableDetail>(item);
                mappedTimeTableDetail.Periods = _periodService.View(mappedTimeTableDetail.Id);
                mappedTimeTableDetailsList.Add(mappedTimeTableDetail);
            }
            return mappedTimeTableDetailsList;
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
