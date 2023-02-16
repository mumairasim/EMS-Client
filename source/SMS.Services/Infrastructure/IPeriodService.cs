using SMS.DTOs.ReponseDTOs;
using System;
using System.Collections.Generic;
using DTOPeriod= SMS.DTOs.DTOs.Period;

namespace SMS.Services.Infrastructure
{
    public interface IPeriodService
    {
        #region SMS Section
        GenericApiResponse Create(DTOPeriod timeTableDetail);
        List<DTOPeriod> View(Guid Id);
        #endregion

    }
}





