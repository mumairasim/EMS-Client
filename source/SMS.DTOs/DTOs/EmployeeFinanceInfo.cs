using System;

namespace SMS.DTOs.DTOs
{
    public class EmployeeFinanceInfo : DtoBaseEntity
    {
        public string FirstName { get; set; }//person
        public string LastName { get; set; }//person
        public string Designation { get; set; }//designation
        public string SchoolName { get; set; }//school
        public string SalaryMonth { get; set; }//empfinance
        public string SalaryYear { get; set; }//empfinance
        public bool IsSalaryTransferred { get; set; }//empfinance
        public Guid EmployeeId { get; set; }//employee
        public Guid? EmpFinanceDetailsId { get; set; }//empfinance
        public Guid? EmpFinanceId { get; set; }//empfinance
    }
}
