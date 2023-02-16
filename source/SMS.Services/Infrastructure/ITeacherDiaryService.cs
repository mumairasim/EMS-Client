using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using System;
using DTOTeacherDiary = SMS.DTOs.DTOs.TeacherDiary;

namespace SMS.Services.Infrastructure
{
    public interface ITeacherDiaryService
    {
        #region SMS Section
        ServiceResponse<DTOTeacherDiary> Get(int pageNumber, int pageSize, string searchString);
        DTOTeacherDiary Get(Guid? id);
        TeacherDiaryResponse Create(DTOTeacherDiary teacherDiary);
        TeacherDiaryResponse Update(DTOTeacherDiary dtoTeacherDiary);
        void Delete(Guid? id, string DeletedBy);
        #endregion

    }
}



