using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SMS.DATA.Infrastructure;
using SMS.Services.Infrastructure;
using DTOStudentAttendanceDetail = SMS.DTOs.DTOs.StudentAttendanceDetail;
using StudentAttendanceDetail = SMS.DATA.Models.StudentAttendanceDetail;

namespace SMS.Services.Implementation
{
    public class StudentAttendanceDetailService : IStudentAttendanceDetailService
    {
        private readonly IRepository<StudentAttendanceDetail> _repository;
        private readonly IMapper _mapper;
        public StudentAttendanceDetailService(IRepository<StudentAttendanceDetail> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #region SMS Section
        public List<DTOStudentAttendanceDetail> GetByStudentAttendanceId(Guid? studentId)
        {
            if (studentId == null) return null;
            var studentAttendanceDetailRecord = _repository.Get().Where(ar => ar.IsDeleted == false && ar.StudentAttendanceId == studentId).ToList();
            if (studentAttendanceDetailRecord.Count <= 0) return null;

            return _mapper.Map<List<StudentAttendanceDetail>, List<DTOStudentAttendanceDetail>>(studentAttendanceDetailRecord);
        }
        public DTOStudentAttendanceDetail Get(Guid? id)
        {
            if (id == null) return null;
            var studentAttendanceDetailRecord = _repository.Get().FirstOrDefault(ar => ar.IsDeleted == false && ar.Id == id);
            if (studentAttendanceDetailRecord == null) return null;

            return _mapper.Map<StudentAttendanceDetail, DTOStudentAttendanceDetail>(studentAttendanceDetailRecord);
        }
        public Guid Create(DTOStudentAttendanceDetail dtoStudentAttendance)
        {
            dtoStudentAttendance.CreatedDate = DateTime.UtcNow;
            dtoStudentAttendance.IsDeleted = false;
            dtoStudentAttendance.Id = Guid.NewGuid();
            dtoStudentAttendance.StudentAttendance = null;
            _repository.Add(_mapper.Map<DTOStudentAttendanceDetail, StudentAttendanceDetail>(dtoStudentAttendance));
            return dtoStudentAttendance.Id;
        }
        public void Create(List<DTOStudentAttendanceDetail> dtoStudentAttendanceDetailList, string createdBy, Guid id)
        {
            foreach (var studentAttendance in dtoStudentAttendanceDetailList)
            {
                studentAttendance.CreatedBy = createdBy;
                studentAttendance.StudentAttendanceId = id;
                Create(studentAttendance);
            }

        }
        public void Update(DTOStudentAttendanceDetail dtoStudentAttendance)
        {
            var studentAttendance = Get(dtoStudentAttendance.Id);
            dtoStudentAttendance.UpdateDate = DateTime.UtcNow;
            var mergedStudentAttendance = _mapper.Map(dtoStudentAttendance, studentAttendance);
            _repository.Update(_mapper.Map<DTOStudentAttendanceDetail, StudentAttendanceDetail>(mergedStudentAttendance));
        }
        public void Delete(Guid? id, string deletedBy)
        {
            if (id == null)
                return;
            var studentAttendance = Get(id);
            studentAttendance.IsDeleted = true;
            studentAttendance.DeletedBy = deletedBy;
            studentAttendance.DeletedDate = DateTime.UtcNow;
            _repository.Update(_mapper.Map<DTOStudentAttendanceDetail, StudentAttendanceDetail>(studentAttendance));
        }
        #endregion

    }
}
