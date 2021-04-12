using System.Collections.Generic;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();

        Employee GetEmployeeById(int employeeId);

        Employee AddEmployee(Employee employee);

        Employee UpdateEmployee(Employee employee);

        void DeleteEmployee(int employeeId);
    }
}