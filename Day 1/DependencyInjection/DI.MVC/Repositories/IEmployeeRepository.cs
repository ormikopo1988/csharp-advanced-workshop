using DI.MVC.Models;
using System.Collections.Generic;

namespace DI.MVC.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee Add(Employee employee);
    }
}