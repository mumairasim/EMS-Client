using System;
using System.Collections.Generic;
using SMS.DTOs.DTOs;
using SMS.DTOs.ReponseDTOs;
using DTOEmployee = SMS.DTOs.DTOs.Employee;

namespace SMS.Services.Infrastructure
{

    public interface IEmployeeService 
    {
        #region SMS Section
        EmployeesList Get(int pageNumber, int pageSize);
        EmployeesList Get(int? employeeNumber, int pageNumber, int pageSize);
        EmployeesList Get(string searchString, int pageNumber, int pageSize);
        DTOEmployee Get(Guid? id);
        List<DTOEmployee> GetEmployeeByDesignation();
        EmployeeResponse Create(DTOEmployee employee);
        EmployeeResponse Update(DTOEmployee dtoEmployee);
        void Delete(Guid? id, string DeletedBy);
        #endregion


    }

}

