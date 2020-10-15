using DI.MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace DI.MVC.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> employees;

        public EmployeeRepository()
        {
            employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john23@gmail.com"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Adams",
                    Email = "jane34@gmail.com"
                },
                new Employee
                {
                    Id = 3,
                    Name = "Scott Pilgrim",
                    Email = "scott25@gmail.com"
                }
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = employees.Max(e => e.Id) + 1;
            employees.Add(employee);

            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            return employees;
        }
    }
}