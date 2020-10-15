using System;
using System.Collections.Generic;

namespace HashSetBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleAdditionsWithValueTypes();
            Console.WriteLine();
            IntersectSets();
            Console.WriteLine();
            UnionSets();
            Console.WriteLine();
            SymmetricExceptWithSets();
            Console.WriteLine();
            AdditionsOfReferenceTypes();
            Console.WriteLine();
            AdditionsOfReferenceTypesThatImplementIEquatableInterface();
        }

        static void SimpleAdditionsWithValueTypes()
        {
            var hashSet = new HashSet<int>();
            hashSet.Add(1);
            hashSet.Add(2);
            hashSet.Add(2);

            foreach (var item in hashSet)
            {
                Console.WriteLine(item);
            }
        }

        static void IntersectSets()
        {
            var set1 = new HashSet<int> { 1, 2, 3 };
            var set2 = new HashSet<int> { 2, 3, 4 };

            set1.IntersectWith(set2);

            foreach (var item in set1)
            {
                Console.Write($"{item}, ");
            }
        }

        static void UnionSets()
        {
            var set1 = new HashSet<int> { 1, 2, 3 };
            var set2 = new HashSet<int> { 2, 3, 4 };

            set1.UnionWith(set2);

            foreach (var item in set1)
            {
                Console.Write($"{item}, ");
            }
        }

        static void SymmetricExceptWithSets()
        {
            var set1 = new HashSet<int> { 1, 2, 3 };
            var set2 = new HashSet<int> { 2, 3, 4 };

            set1.SymmetricExceptWith(set2);

            foreach (var item in set1)
            {
                Console.Write($"{item}, ");
            }
        }

        static void AdditionsOfReferenceTypes()
        {
            var hashSet = new HashSet<Employee>();

            hashSet.Add(new Employee
            {
                Name = "John"
            });
            hashSet.Add(new Employee
            {
                Name = "Jane"
            });
            hashSet.Add(new Employee
            {
                Name = "John"
            });

            foreach (var item in hashSet)
            {
                Console.WriteLine(item.Name);
            }
        }

        static void AdditionsOfReferenceTypesThatImplementIEquatableInterface()
        {
            var employees = new HashSet<EquatableEmployee>
            {
                new EquatableEmployee
                {
                    Id = 1,
                    Name = "John"
                },
                new EquatableEmployee
                {
                    Id = 2,
                    Name = "Jane"
                },
                new EquatableEmployee
                {
                    Id = 1,
                    Name = "John"
                }
            };

            foreach (var employee in employees)
            {
                Console.WriteLine($"Employee Id: {employee.Id} | Employee Name: {employee.Name}");
            }
        }
    }
}