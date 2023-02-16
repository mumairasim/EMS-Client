using SMS.DATA.Models.NonDbContextModels;
using System;
using System.Collections.Generic;

namespace SMS.DATA.Infrastructure
{
    public interface IStoredProcCaller
    {
        #region Stroed
        UserInfo GetUserInfo(string UserName);
        List<StudentFinanceInfo> GetStudentFinance(Guid? schoolId, Guid? ClassId, Guid? StudentId, string FeeMonth);
        List<EmployeeFinanceInfo> GetEmployeeFinance(Guid? schoolId, Guid? DesignationId, string SalaryMonth);
        List<EmployeeFinanceInfo> GetEmployeeFinanceDetail(Guid? schoolId, Guid? DesignationId);
        List<StudentFinanceInfo> GetStudentFinanceDetail(Guid? schoolId, Guid? ClassId, int? Regno, string Month, string Year);
        #endregion

        #region Request Stroed
        UserInfo RequestGetUserInfo(string UserName);
        List<StudentFinanceInfo> RequestGetStudentFinance(Guid? schoolId, Guid? ClassId, Guid? StudentId, string FeeMonth);
        List<EmployeeFinanceInfo> RequestGetEmployeeFinance(Guid? schoolId, Guid? DesignationId, string SalaryMonth);
        List<EmployeeFinanceInfo> RequestGetEmployeeFinanceDetail(Guid? schoolId, Guid? DesignationId);
        List<StudentFinanceInfo> RequestGetStudentFinanceDetail(Guid? schoolId, Guid? ClassId, Guid? StudentId);
        #endregion
    }
}
