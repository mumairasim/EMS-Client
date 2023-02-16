using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.Services.Infrastructure;
using TimeTable = SMS.DATA.Models.TimeTable;
using DTOTimeTable = SMS.DTOs.DTOs.TimeTable;
using SMS.DTOs.ReponseDTOs;
using RequestMeta = SMS.DATA.Models.RequestMeta;

namespace SMS.Services.Implementation
{
    public class TimeTableService : ITimeTableService
    {
        private readonly IRepository<TimeTable> _repository;
        private readonly IMapper _mapper;
        private readonly ITimeTableDetailService _timeTableDetailService;
        public IRequestMetaService _requestMetaService;

        public TimeTableService(IRepository<TimeTable> repository, IMapper mapper, ITimeTableDetailService timeTableDetailService, IRequestMetaService requestMetaService = null)
        {
            _repository = repository;
            _mapper = mapper;
            _timeTableDetailService = timeTableDetailService;
            _requestMetaService = requestMetaService;
        }
        #region SMS Section
        public TimeTableList Get(Guid? schoolId, Guid? classId, int pageNumber, int pageSize)
        {
            var timeTables = _repository.Get().Where(tt => tt.IsDeleted == false && tt.SchoolId == schoolId && tt.ClassId == classId).OrderByDescending(lp => lp.CreatedDate).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var timeTableCount = _repository.Get().Count(st => st.IsDeleted == false);
            var timeTableList = new List<DTOTimeTable>();
            foreach (var timeTable in timeTables)
            {
                timeTableList.Add(_mapper.Map<TimeTable, DTOTimeTable>(timeTable));
            }
            var timeTablesList = new TimeTableList()
            {
                TimeTables = timeTableList,
                TimeTablesCount = timeTableCount
            };
            return timeTablesList;
        }
        public GenericApiResponse Create(DTOTimeTable dtoTimeTable)
        {
            try
            {
                dtoTimeTable.CreatedDate = DateTime.Now;
                dtoTimeTable.IsDeleted = false;
                if (dtoTimeTable.Id == Guid.Empty)
                {
                    dtoTimeTable.Id = Guid.NewGuid();
                }

                HelpingMethodForRelationship(dtoTimeTable);
                var timeTable = _repository.Add(_mapper.Map<DTOTimeTable, TimeTable>(dtoTimeTable));
                if (dtoTimeTable.TimeTableDetails != null)
                    foreach (var timeTableDetail in dtoTimeTable.TimeTableDetails)
                    {
                        timeTableDetail.TimeTableId = timeTable.Id;
                        timeTableDetail.CreatedBy = dtoTimeTable.CreatedBy;
                        _timeTableDetailService.Create(timeTableDetail);
                    }
                if (dtoTimeTable.IsClient == true)
                {
                    _requestMetaService.Create(new RequestMeta
                    {
                        ModuleId = dtoTimeTable.Id,
                        SchoolId = dtoTimeTable.SchoolId,
                        ModuleName = DATA.Models.Enums.Module.TimeTable,
                        ApprovalStatus = DATA.Models.Enums.RequestStatus.Pending,
                        Type = DATA.Models.Enums.RequestType.Create
                    });
                }
                return PrepareSuccessResponse("success", "");
            }
            catch (Exception e)
            {
                return PrepareFailureResponse("error", e.Message);
            }

        }
        public DTOTimeTable View(Guid Id)
        {
            var timeTable = _repository.Get().FirstOrDefault(tt => tt.IsDeleted == false && tt.Id == Id);
            var mappedTimeTable = _mapper.Map<TimeTable, DTOTimeTable>(timeTable);
            mappedTimeTable.TimeTableDetails = _timeTableDetailService.View(mappedTimeTable.Id);
            return mappedTimeTable;
        }
        private void HelpingMethodForRelationship(DTOTimeTable dtoTimeTable)
        {
            dtoTimeTable.SchoolId = dtoTimeTable.School.Id;
            dtoTimeTable.School = null;
            dtoTimeTable.ClassId = dtoTimeTable.Class.Id;
            dtoTimeTable.Class = null;
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
