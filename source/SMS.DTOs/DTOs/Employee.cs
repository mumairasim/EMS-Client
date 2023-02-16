
using System;

namespace SMS.DTOs.DTOs
{
    public class Employee : DtoBaseEntity
    {
        public int? EmployeeNumber { get; set; }
        public Guid? PersonId { get; set; }

        public Guid? DesignationId { get; set; }

        public Person Person { get; set; }

        public Designation Designation { get; set; }

        public decimal? MonthlySalary { get; set; }

    }
}
