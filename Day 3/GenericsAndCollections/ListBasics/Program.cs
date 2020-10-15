using System;
using System.Collections.Generic;

namespace ListBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            ListCapacity();
            AddItemsAndIterateThroughList();
            AddCollectionToAList();
            RemoveItemsFromList();
            FindItemFromList();
        }

        static void ListCapacity()
        {
            //var numbers = new List<int>();
            var numbers = new List<int>(10); // You can also pass the initial capacity of the List. The algorithm of resizing states the same. The only that changes is the initial capacity

            var capacity = -1;

            while (capacity < 100)
            {
                if (numbers.Capacity != capacity)
                {
                    capacity = numbers.Capacity;
                    Console.WriteLine($"List capacity is: {capacity}");
                }

                numbers.Add(1);
            }
        }

        static void AddItemsAndIterateThroughList()
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    Name = "John"
                },
                new Employee
                {
                    Name = "Jane"
                }
            };

            employees.Add(new Employee
            {
                Name = "Scott"
            });

            foreach (var emp in employees)
            {
                Console.WriteLine(emp.Name);
            }

            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine(employees[i].Name);
            }
        }

        static void AddCollectionToAList()
        {
            // Collection of string  
            string[] animals = { "Cow", "Camel", "Elephant" };

            // Create a List and add a collection  
            var animalsList = new List<string>();

            animalsList.AddRange(animals);
            
            foreach (string a in animalsList)
            {
                Console.WriteLine(a);
            }
        }

        static void RemoveItemsFromList()
        {
            var tmpStrs = new List<string>
            {
                "Str1", "Str2", "Str3", "Str4", "Str5", "Str6", "Str7", "Str8", "Str9", "Str10"
            };

            // Remove 3rd item - Str3
            tmpStrs.RemoveAt(3);

            // Remove an item  
            tmpStrs.Remove("Str6");

            // Remove a range  
            tmpStrs.RemoveRange(3, 2);

            foreach (var str in tmpStrs)
            {
                Console.WriteLine(str);
            }
        }

        static void FindItemFromList()
        {
            // List of string  
            var authors = new List<string>(5);
            authors.Add("Mahesh Chand");
            authors.Add("Chris Love");
            authors.Add("Allen O'neill");
            authors.Add("Naveen Sharma");
            authors.Add("Mahesh Chand");
            authors.Add("Monica Rathbun");
            authors.Add("David McCarter");

            int idx = authors.IndexOf("Naveen Sharma");

            if (idx > 0)
                Console.WriteLine($"Item index in List is: {idx}");
            else
                Console.WriteLine("Item not found");

            Console.WriteLine(authors.IndexOf("Naveen Sharma", 2));
            Console.WriteLine(authors.LastIndexOf("Mahesh Chand"));
        }
    }
}