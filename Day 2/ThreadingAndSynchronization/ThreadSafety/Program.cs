using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThreadSafety
{
    public class Program
    {
        private static readonly object customLock = new();
        private static readonly Dictionary<int, string> items = [];

        public static void Main()
        {
            var task1 = Task.Factory.StartNew(AddItem);
            var task2 = Task.Factory.StartNew(AddItem);
            var task3 = Task.Factory.StartNew(AddItem);
            var task4 = Task.Factory.StartNew(AddItem);
            var task5 = Task.Factory.StartNew(AddItem);

            Task.WaitAll(task3, task1, task2, task4, task5);

            Console.ReadLine();
        }

        private static void AddItem()
        {
            lock (customLock)
            {
                Console.WriteLine("Write lock acquired by thread #{0}.", Environment.CurrentManagedThreadId);
                items.Add(items.Count, "Test Value " + items.Count);
            }

            lock (customLock)
            {
                Console.WriteLine("Read lock acquired by thread #{0}.", Environment.CurrentManagedThreadId);

                foreach (var item in items)
                {
                    Console.WriteLine(item.Key + ": " + item.Value);
                }
            }
        }
    }
}