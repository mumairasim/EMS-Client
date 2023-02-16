using AutoMapper;
using MoreLinq;
using SMS.DATA.Infrastructure;
using SMS.DTOs.DTOs;
using SMS.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using DBDesignation = SMS.DATA.Models.Designation;
using DBEmployee = SMS.DATA.Models.Employee;
using DBEmployeeFinance = SMS.DATA.Models.EmployeeFinance;
using DBEmployeeFinanceDetail = SMS.DATA.Models.EmployeeFinanceDetail;
using DBPerson = SMS.DATA.Models.Person;
using DBSchool = SMS.DATA.Models.School;
using DTOEmployeeFinanceInfo = SMS.DTOs.DTOs.EmployeeFinanceInfo;

namespace SMS.Services.Implementation
{
    public class EmployeeFinanceService : IEmployeeFinanceService
    {
        #region Properties
        private readonly IMapper _mapper;
        private readonly IRepository<DBEmployeeFinance> _repositoryEmpFinance;
        private readonly IRepository<DBPerson> _repositoryPerson;
        private readonly IRepository<DBDesignation> _repositoryDesignation;
        private readonly IRepository<DBEmployee> _repositoryEmployee;
        private readonly IRepository<DBSchool> _repositorySchool;
        private readonly IRepository<DBEmployeeFinanceDetail> _repositoryFinanceDetail;

        public EmployeeFinanceService(
              IRepository<DBEmployeeFinance> repositoryEmpFinance,
              IRepository<DBPerson> repositoryPerson,
              IRepository<DBDesignation> repositoryDesignation,
              IRepository<DBSchool> repositorySchool,
              IRepository<DBEmployeeFinanceDetail> repositoryFinanceDetail,
            IRepository<DBEmployee> repositoryEmployee,
            IMapper mapper)
        {
            _mapper = mapper;
            _repositoryEmployee = repositoryEmployee;
            _repositoryEmpFinance = repositoryEmpFinance;
            _repositoryPerson = repositoryPerson;
            _repositoryDesignation = repositoryDesignation;
            _repositorySchool = repositorySchool;
            _repositoryFinanceDetail = repositoryFinanceDetail;
        }


        #endregion

        #region Init



        #endregion
        public List<DTOEmployeeFinanceInfo> GetByFilter(Guid? schoolId, Guid? DesignationId, string SalaryMonth)
        {

            var list = (from person in _repositoryPerson.Get()
                        join employee in _repositoryEmployee.Get() on person.Id equals employee.PersonId
                        join designation in _repositoryDesignation.Get().Where(x => DesignationId == null || DesignationId == x.Id) on employee.DesignationId equals designation.Id
                        join school in _repositorySchool.Get().Where(x => schoolId == null || schoolId == x.Id) on employee.SchoolId equals school.Id
                        join empFinDet in _repositoryFinanceDetail.Get() on employee.Id equals empFinDet.EmployeeId into tempFinDet
                        from empFinDet in tempFinDet.DefaultIfEmpty()
                        join empFin in _repositoryEmpFinance.Get().Where(x => string.IsNullOrEmpty(SalaryMonth) || x.SalaryMonth.Contains(SalaryMonth)) on empFinDet.Id equals empFin.EmployeeFinanceDetailsId //into empFinTemp
                        select new DTOEmployeeFinanceInfo
                        {
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            EmployeeId = employee.Id,
                            Designation = designation.Name,
                            IsSalaryTransferred = empFin.SalaryTransfered ?? false,
                            SalaryMonth = empFin.SalaryMonth,
                            SalaryYear = empFin.SalaryYear,
                            EmpFinanceDetailsId = empFinDet.Id,
                            SchoolName = school.Name
                        }).ToList();

            return list;
        }

        public List<DTOEmployeeFinanceInfo> GetDetailByFilter(Guid? schoolId, Guid? DesignationId)
        {
            var list = (from person in _repositoryPerson.Get()
                        join employee in _repositoryEmployee.Get().Where(x => x.IsDeleted == false) on person.Id equals employee.PersonId
                        join designation in _repositoryDesignation.Get().Where(x => DesignationId == null || DesignationId == x.Id) on employee.DesignationId equals designation.Id
                        join school in _repositorySchool.Get().Where(x => schoolId == null || schoolId == x.Id) on employee.SchoolId equals school.Id
                        join empFinDet in _repositoryFinanceDetail.Get() on employee.Id equals empFinDet.EmployeeId
                        select new DTOEmployeeFinanceInfo
                        {
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            EmployeeId = employee.Id,
                            Designation = designation.Name,
                            EmpFinanceDetailsId = empFinDet.Id,
                            SchoolName = school.Name,
                        }).DistinctBy(x => x.EmployeeId).ToList();
            return list;
        }

        public EmployeeFinanceDetail GetFinanceDetailByEmployeeId(Guid empId)
        {
            var singleFinanceDetail = _repositoryFinanceDetail.Get().FirstOrDefault(x => x.EmployeeId == empId && (x.IsDeleted == false || x.IsDeleted == null));
            return _mapper.Map<DBEmployeeFinanceDetail, EmployeeFinanceDetail>(singleFinanceDetail);
        }

        public bool IsSalaryTransferredByMonthAndYear(Guid Empid, string month, string year)
        {
            var isExists = (from fin in _repositoryEmpFinance.Get().Where(x => x.SalaryMonth.Contains(month) && x.SalaryYear == year)
                            join empFinDet in _repositoryFinanceDetail.Get() on fin.EmployeeFinanceDetailsId equals empFinDet.Id
                            where empFinDet.EmployeeId == Empid
                            select fin).Any();
            return isExists;
        }
        public void Create(DTOEmployeeFinanceInfo employeeFinanceInfo)
        {
            var newFinance = new DBEmployeeFinance
            {
                EmployeeFinanceDetailsId = employeeFinanceInfo.EmpFinanceDetailsId,
                SalaryTransfered = employeeFinanceInfo.IsSalaryTransferred,
                SalaryMonth = employeeFinanceInfo.SalaryMonth,
                CreatedDate = DateTime.UtcNow,
                SalaryYear = employeeFinanceInfo.SalaryYear,
                IsDeleted = false,
                CreatedBy = employeeFinanceInfo.CreatedBy
            };

            if (employeeFinanceInfo.Id == Guid.Empty)
            {
                newFinance.Id = Guid.NewGuid();
            }
            if ((newFinance.SalaryTransfered ?? false) &&
                !IsSalaryTransferredByMonthAndYear(
                employeeFinanceInfo.EmployeeId,
                employeeFinanceInfo.SalaryMonth,
                employeeFinanceInfo.SalaryYear))
            {
                _repositoryEmpFinance.Add(newFinance);
            }
        }

        public void CreateFinanceDetails(EmployeeFinanceDetail dTOEmployeeFinanceDetail)
        {
            dTOEmployeeFinanceDetail.CreatedDate = DateTime.UtcNow;
            dTOEmployeeFinanceDetail.IsDeleted = false;
            if (dTOEmployeeFinanceDetail.Id == Guid.Empty)
            {
                dTOEmployeeFinanceDetail.Id = Guid.NewGuid();
            }
            _repositoryFinanceDetail.Add(_mapper.Map<EmployeeFinanceDetail, DBEmployeeFinanceDetail>(dTOEmployeeFinanceDetail));
        }

        public void UpdateFinanceDetail(EmployeeFinanceDetail dTOEmployeeFinanceDetail)
        {
            var financeDetails = GetFinanceDetailByEmployeeId(dTOEmployeeFinanceDetail.EmployeeId ?? Guid.Empty);
            if (financeDetails != null)
            {
                financeDetails.UpdateDate = DateTime.UtcNow;
                financeDetails.IsDeleted = false;
                var updated = _mapper.Map(dTOEmployeeFinanceDetail, financeDetails);
                var updatedDbRec = _mapper.Map<EmployeeFinanceDetail, DBEmployeeFinanceDetail>(updated);
                _repositoryFinanceDetail.Update(updatedDbRec);
            }
        }
    }
}
