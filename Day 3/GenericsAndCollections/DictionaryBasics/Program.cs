using System;
using System.Collections.Generic;

namespace DictionaryBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicDictionaryOperations();
            SearchDictionaryWithKey();
            RemoveFromDictionaryByKey();
            GetCollectionOfKeys();
            GetCollectionOfValues();
        }

        static void BasicDictionaryOperations()
        {
            var employees = new Dictionary<string, Employee>();

            employees.Add("John", new Employee
            {
                Name = "John"
            });
            employees.Add("Jane", new Employee
            {
                Name = "Jane"
            });
            employees.Add("Scott", new Employee
            {
                Name = "Scott"
            });

            var scott = employees["Scott"];

            Console.WriteLine($"Scott emp name: {scott.Name}");

            foreach (var item in employees)
            {
                Console.WriteLine($"{item.Key}:{item.Value.Name}");
            }
        }

        static void SearchDictionaryWithKey()
        {
            var map = new Dictionary<int, string>();
            map.Add(1, "one");
            map.Add(2, "two");

            Console.WriteLine(map.ContainsKey(1));
        }

        static void RemoveFromDictionaryByKey()
        {
            var map = new Dictionary<int, string>();
            map.Add(1, "one");
            map.Add(2, "two");

            map.Remove(1);

            foreach (var item in map)
            {
                Console.WriteLine($"{item.Key}:{item.Value}");
            }
        }

        static void GetCollectionOfKeys()
        {
            var map = new Dictionary<int, string>();
            map.Add(1, "one");
            map.Add(2, "two");

            foreach (var key in map.Keys)
            {
                Console.WriteLine("Key: {0}", key);
            }
        }

        static void GetCollectionOfValues()
        {
            var map = new Dictionary<int, string>();
            map.Add(1, "one");
            map.Add(2, "two");

            foreach (var value in map.Values)
            {
                Console.WriteLine("Value: {0}", value);
            }
        }
    }
}