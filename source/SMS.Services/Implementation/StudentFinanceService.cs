using AutoMapper;
using MoreLinq;
using SMS.DATA.Infrastructure;
using SMS.DATA.Models;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using DBStudentFinances = SMS.DATA.Models.Student_Finances;
using DTOStudentFinanceCustom = SMS.DTOs.DTOs.StudentFinanceInfo;
using DTOStudentFinances = SMS.DTOs.DTOs.Student_Finances;
namespace SMS.Services.Implementation
{
    public class StudentFinanceService : IStudentFinanceService
    {
        #region Properties
        private readonly IRepository<DBStudentFinances> _repositoryStuFin;
        private readonly IRepository<Person> _repositoryPerson;
        private readonly IRepository<Student> _repositoryStudent;
        private readonly IRepository<Class> _repositoryClass;
        private readonly IRepository<StudentFinanceDetail> _repositoryStuFinDetails;
        private readonly IRepository<School> _repositorySchool;
        private readonly IRepository<FinanceType> _repositoryFinType;
        private readonly IStudentFinanceDetailsService _studentFinanceDetailsService;
        private readonly IMapper _mapper;
        #endregion

        #region Init
        public StudentFinanceService(IRepository<DBStudentFinances> repository, IRepository<Person> repositoryPerson, IRepository<Student> repositoryStudent, IRepository<Class> repositoryClass, IRepository<StudentFinanceDetail> repositoryStuFinDetails, IRepository<School> repositorySchool, IRepository<FinanceType> repositoryFinType, IStudentFinanceDetailsService studentFinanceDetailsService, IMapper mapper)
        {
            _repositoryStuFin = repository;
            _repositoryPerson = repositoryPerson;
            _repositoryStudent = repositoryStudent;
            _repositoryClass = repositoryClass;
            _repositoryStuFinDetails = repositoryStuFinDetails;
            _repositorySchool = repositorySchool;
            _repositoryFinType = repositoryFinType;
            _studentFinanceDetailsService = studentFinanceDetailsService;
            _mapper = mapper;
        }

        #endregion

        #region Service Calls

        /// <summary>
        /// Service level call : Creates a single record of a StudentFinances
        /// </summary>
        /// <param name="dtoStudentFinances"></param>
        public void Create(DTOStudentFinanceCustom dtoStudentFinances)
        {
            var newFinance = new DBStudentFinances
            {
                StudentFinanceDetailsId = dtoStudentFinances.StudentFinanceDetailsId,
                FeeSubmitted = dtoStudentFinances.FeeSubmitted,
                FeeMonth = dtoStudentFinances.FeeMonth,
                CreatedDate = DateTime.UtcNow,
                FeeYear = dtoStudentFinances.FeeYear,
                IsDeleted = false,
                CreatedBy = dtoStudentFinances.CreatedBy
            };

            if (newFinance.Id == Guid.Empty)
            {
                newFinance.Id = Guid.NewGuid();
            }

            if ((newFinance.FeeSubmitted ?? false) &&
                !IsFeeTransferredByMonthAndYear(
                    dtoStudentFinances.StudentId,
                    dtoStudentFinances.FeeMonth,
                    dtoStudentFinances.FeeYear)
                )
            {
                _repositoryStuFin.Add(newFinance);
            }

        }

        /// <summary>
        /// Service level call : Delete a single record of a StudentFinances
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid? id)
        {
            if (id == null)
                return;
            var StudentFinances = Get(id);
            if (StudentFinances != null)
            {
                StudentFinances.IsDeleted = true;
                StudentFinances.DeletedDate = DateTime.UtcNow;
                _repositoryStuFin.Update(_mapper.Map<DTOStudentFinances, DBStudentFinances>(StudentFinances));
            }

        }

        /// <summary>
        /// Retruns a Single Record of a StudentFinances
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DTOStudentFinances Get(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var StudentFinances = _repositoryStuFin.Get().FirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
            var StudentFinancesDto = _mapper.Map<DBStudentFinances, DTOStudentFinances>(StudentFinances);

            return StudentFinancesDto;
        }

        public List<DTOStudentFinanceCustom> GetByFilter(Guid? schoolId, Guid? classId, Guid? studentId, string feeMonth)
        {
            var list = (from person in _repositoryPerson.Get()
                        join student in _repositoryStudent.Get().Where(x => x.IsDeleted == false) on person.Id equals student.PersonId
                        join stuFinDet in _repositoryStuFinDetails.Get().Where(x => x.IsDeleted == false) on student.Id equals stuFinDet.StudentId
                        join finType in _repositoryFinType.Get().Where(x => x.IsDeleted == false) on stuFinDet.FinanceTypeId equals finType.Id
                        join stuFin in _repositoryStuFin.Get().Where(x => (x.IsDeleted == false) && string.IsNullOrEmpty(feeMonth) || x.FeeMonth.Contains(feeMonth)) on stuFinDet.Id equals stuFin.StudentFinanceDetailsId
                        join school in _repositorySchool.Get().Where(x => schoolId == null || schoolId == x.Id) on student.SchoolId equals school.Id
                        join cls in _repositoryClass.Get().Where(x => (x.IsDeleted == false) && classId == null || classId == x.Id) on student.ClassId equals cls.Id
                        select new DTOStudentFinanceCustom
                        {
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            FeeMonth = stuFin.FeeMonth,
                            FeeYear = stuFin.FeeYear,
                            StudentFinanceDetailsId = stuFinDet.Id,
                            FeeSubmitted = stuFin.FeeSubmitted ?? false,
                            RegistrationNumber = student.RegistrationNumber,
                            SchoolName = school.Name,
                            SchoolId = school.Id,
                            StudentId = student.Id,
                            Fee = stuFinDet.Fee,
                            Arears = stuFinDet.Arears,
                            ClassName = cls.ClassName,
                            Type = finType.Type,
                            StudentFinanceId = stuFin.Id,
                            ClassId = cls.Id
                        }).DistinctBy(x => x.StudentFinanceId).ToList();
            return list;
        }

        public bool IsFeeTransferredByMonthAndYear(Guid stuId, string month, string year)
        {
            var isExists = (from fin in _repositoryStuFin.Get().Where(x => x.FeeMonth.Contains(month) && x.FeeYear == year)
                            join stuFinDet in _repositoryStuFinDetails.Get() on fin.StudentFinanceDetailsId equals stuFinDet.Id
                            where stuFinDet.StudentId == stuId
                            select fin).Any();
            return isExists;
        }

        public List<DTOStudentFinanceCustom> GetDetailByFilter(Guid? schoolId, Guid? ClassId, int? Regno, string Month, string Year)
        {
            var list = (from person in _repositoryPerson.Get()
                        join student in _repositoryStudent.Get().Where(x => x.IsDeleted == false) on person.Id equals student.PersonId
                        join stuFinDet in _repositoryStuFinDetails.Get().Where(x => x.IsDeleted == false) on student.Id equals stuFinDet.StudentId
                        join school in _repositorySchool.Get().Where(x => schoolId == null || schoolId == x.Id) on student.SchoolId equals school.Id
                        join cls in _repositoryClass.Get().Where(x => (x.IsDeleted == false) && ClassId == null || ClassId == x.Id) on student.SchoolId equals cls.SchoolId
                        select new DTOStudentFinanceCustom
                        {
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            StudentFinanceDetailsId = stuFinDet.Id,
                            RegistrationNumber = student.RegistrationNumber,
                            SchoolName = school.Name,
                            SchoolId = school.Id,
                            StudentId = student.Id,
                            Fee = stuFinDet.Fee,
                            Arears = stuFinDet.Arears,
                            ClassName = cls.ClassName
                        }).DistinctBy(x => x.StudentId).ToList();
            return list;
        }


        /// <summary>
        /// Service level call : Updates the Single Record of a StudentFinances 
        /// </summary>
        /// <param name="DTOStudentFinances"></param>
        public void Update(DTOStudentFinanceCustom dtoStudentFinances)
        {
            var newFinance = new DBStudentFinances
            {
                StudentFinanceDetailsId = dtoStudentFinances.StudentFinanceDetailsId,
                FeeSubmitted = dtoStudentFinances.FeeSubmitted,
                FeeMonth = dtoStudentFinances.FeeMonth,
                Arears = dtoStudentFinances.Arears,
                CreatedDate = DateTime.UtcNow,
                FeeYear = dtoStudentFinances.FeeYear,
                IsDeleted = false,
                CreatedBy = dtoStudentFinances.CreatedBy
            };

            if (newFinance.Id == Guid.Empty)
            {
                newFinance.Id = Guid.NewGuid();
            }

            if (newFinance.FeeSubmitted ?? false)
            {
                _repositoryStuFin.Add(newFinance);
                var studentFinanceDetails = _studentFinanceDetailsService.Get(dtoStudentFinances.StudentFinanceDetailsId);
                studentFinanceDetails.Arears = dtoStudentFinances.Arears;
                _studentFinanceDetailsService.Update(studentFinanceDetails);
            }
        }

        /// <summary>
        /// Service level call : Return all records of a StudentFinances
        /// </summary>
        /// <returns></returns>
        List<DTOStudentFinances> IStudentFinanceService.GetAll()
        {
            var StudentFinancess = _repositoryStuFin.Get().Where(x => (x.IsDeleted == false || x.IsDeleted == null)).ToList();
            var StudentFinancesList = new List<DTOStudentFinances>();
            foreach (var StudentFinances in StudentFinancess)
            {
                StudentFinancesList.Add(_mapper.Map<DBStudentFinances, DTOStudentFinances>(StudentFinances));
            }
            return StudentFinancesList;
        }



        #endregion
    }
}
