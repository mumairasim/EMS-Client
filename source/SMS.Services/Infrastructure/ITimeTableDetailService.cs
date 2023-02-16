using SMS.DTOs.ReponseDTOs;
using System;
using System.Collections.Generic;
using DTOTimeTableDetail = SMS.DTOs.DTOs.TimeTableDetail;

namespace SMS.Services.Infrastructure
{
    public interface ITimeTableDetailService
    {
        #region SMS
        GenericApiResponse Create(DTOTimeTableDetail timeTableDetail);
        List<DTOTimeTableDetail> View(Guid Id);
        #endregion

    }
}





