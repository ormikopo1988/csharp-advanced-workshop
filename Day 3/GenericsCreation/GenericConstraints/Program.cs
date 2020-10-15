using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenericConstraints
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BaseClassConstraintDemo();
            await ReferenceTypeConstraintDemoAsync();

            var result = EnumConstraintDemo<RainbowColor>();

            foreach (var item in result)
            {
                Console.WriteLine($"Key: {item.Key} - Value: {item.Value}");
            }
        }

        static void BaseClassConstraintDemo()
        {
            var employeeList = new GenericList<Employee>();

            employeeList.AddHead(new Employee("John", 1));
            employeeList.AddHead(new Employee("Jane", 2));
            employeeList.AddHead(new Employee("Scott", 3));
            employeeList.AddHead(new Manager("Philip", 4));

            foreach (var employee in employeeList)
            {
                Console.WriteLine($"ID: {employee.ID} - Name: {employee.Name}");
            }
        }

        static async Task ReferenceTypeConstraintDemoAsync()
        {
            var httpClientService = new HttpClientService();

            var response = await httpClientService.GetRequestAsync<List<User>>("https://jsonplaceholder.typicode.com/users");

            foreach (var user in response)
            {
                Console.WriteLine($"User Id: {user.Id} - Name: {user.Name}");
            }
        }

        static Dictionary<int, string> EnumConstraintDemo<T>() 
            where T : Enum
        {
            var result = new Dictionary<int, string>();
            var values = Enum.GetValues(typeof(T));

            foreach (int item in values)
            {
                result.Add(item, Enum.GetName(typeof(T), item));
            }
                
            return result;
        }
    }
}