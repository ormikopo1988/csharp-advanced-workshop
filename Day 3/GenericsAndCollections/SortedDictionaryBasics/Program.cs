using System;
using System.Collections.Generic;

namespace SortedDictionaryBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeesByDepartment = new SortedDictionary<string, List<Employee>>();

            employeesByDepartment.Add("Sales", new List<Employee> { new Employee(), new Employee() });
            employeesByDepartment.Add("Engineering", new List<Employee> { new Employee(), new Employee(), new Employee() });

            foreach (var item in employeesByDepartment)
            {
                Console.WriteLine($"The count of employees for {item.Key} department is {item.Value.Count}");
            }
        }
    }
}