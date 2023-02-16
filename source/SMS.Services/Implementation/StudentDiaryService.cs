using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using DTOStudentDiary = SMS.DTOs.DTOs.StudentDiary;
using StudentDiary = SMS.DATA.Models.StudentDiary;


namespace SMS.Services.Implementation
{
    public class StudentDiaryService : IStudentDiaryService
    {
        private readonly IRepository<StudentDiary> _repository;
        private readonly IMapper _mapper;
        public StudentDiaryService(IRepository<StudentDiary> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #region SMS Section
        public void Create(DTOStudentDiary dtoStudentDiary)
        {
            dtoStudentDiary.CreatedDate = DateTime.UtcNow;
            dtoStudentDiary.IsDeleted = false;
            if (dtoStudentDiary.Id == Guid.Empty)
            {
                dtoStudentDiary.Id = Guid.NewGuid();
            }
            HelpingMethodForRelationship(dtoStudentDiary);
            _repository.Add(_mapper.Map<DTOStudentDiary, StudentDiary>(dtoStudentDiary));
        }
        public ServiceResponse<DTOStudentDiary> Get(string searchString, int pageNumber, int pageSize)
        {
            var resultSet = _repository.Get()
                .Where(cl => string.IsNullOrEmpty(searchString) || cl.DiaryText.ToLower().Contains(searchString.ToLower()))
                .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.School.Name.ToLower().Contains(searchString.ToLower())))
                .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.Employee.Person.FirstName.ToLower().Contains(searchString.ToLower())))
                .Union(_repository.Get().Where(cl => string.IsNullOrEmpty(searchString) || cl.Employee.Person.LastName.ToLower().Contains(searchString.ToLower())))
                .Where(cl => cl.IsDeleted == false).OrderByDescending(st => st.Id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var resultCount = resultSet.Count;
            var tempList = new List<DTOStudentDiary>();
            foreach (var item in resultSet)
            {
                tempList.Add(_mapper.Map<StudentDiary, DTOStudentDiary>(item));
            }
            var finalList = new ServiceResponse<DTOStudentDiary>()
            {
                Items = tempList,
                Count = resultCount
            };
            return finalList;
        }
        public DTOStudentDiary Get(Guid? id)
        {
            if (id == null) return null;
            var studentDiaryRecord = _repository.Get().FirstOrDefault(cl => cl.Id == id && cl.IsDeleted == false);
            var studentdiary = _mapper.Map<StudentDiary, DTOStudentDiary>(studentDiaryRecord);

            return studentdiary;
        }
        public void Update(DTOStudentDiary dtoStudentDiary)
        {
            var studentDiary = Get(dtoStudentDiary.Id);
            dtoStudentDiary.UpdateDate = DateTime.UtcNow;
            var mergedstudentDiary = _mapper.Map(dtoStudentDiary, studentDiary);
            _repository.Update(_mapper.Map<DTOStudentDiary, StudentDiary>(mergedstudentDiary));
        }
        public void Delete(Guid? id, string DeletedBy)
        {
            if (id == null)
                return;
            var studentDiary = Get(id);
            studentDiary.IsDeleted = true;
            studentDiary.DeletedBy = DeletedBy;

            studentDiary.DeletedDate = DateTime.UtcNow;
            _repository.Update(_mapper.Map<DTOStudentDiary, StudentDiary>(studentDiary));
        }
        #endregion


        #region private
        private void HelpingMethodForRelationship(DTOStudentDiary dtoStudentDiary)
        {
            dtoStudentDiary.InstructorId = dtoStudentDiary.Employee.Id;
            dtoStudentDiary.SchoolId = dtoStudentDiary.School.Id;
            dtoStudentDiary.School = null;
            dtoStudentDiary.Employee = null;
        }
        #endregion
    }


}

