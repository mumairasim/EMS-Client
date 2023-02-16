using System;
using System.Collections.Generic;
using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using DTOLessonPlan = SMS.DTOs.DTOs.LessonPlan;

namespace SMS.Services.Infrastructure
{
    public interface ILessonPlanService
    {
        #region SMS Section
        LessonPlansList Get(int pageNumber, int pageSize);
        //List<DTOLessonPlan> Get();
        DTOLessonPlan Get(Guid? id);
        LessonPlanResponse Create(DTOLessonPlan lessonplan);
        LessonPlanResponse Update(DTOLessonPlan dtolessonplan);
        void Delete(Guid? id, string DeletedBy);
        #endregion
    }
}


