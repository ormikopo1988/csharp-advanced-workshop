using System;
using System.Collections.Generic;

namespace QueueBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueBasicOperations();
            //QueueContainOperation();
            //ClearQueue();
        }

        static void QueueBasicOperations()
        {
            var line = new Queue<Employee>();

            line.Enqueue(new Employee
            {
                Name = "John"
            });
            line.Enqueue(new Employee
            {
                Name = "Jane"
            });
            line.Enqueue(new Employee
            {
                Name = "Scott"
            });

            var firstEmpInQueue = line.Peek();

            Console.WriteLine($"First employee in the queue is: {firstEmpInQueue.Name}");

            while (line.Count > 0)
            {
                var employee = line.Dequeue();

                Console.WriteLine(employee.Name);
            }
        }

        static void QueueContainOperation()
        {
            var queue1 = new Queue<string>();
            queue1.Enqueue("MCA");
            queue1.Enqueue("MBA");
            queue1.Enqueue("BCA");
            queue1.Enqueue("BBA");
            
            Console.WriteLine("The elements in the queue are:");
            
            foreach (string s in queue1)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("The element MCA is in the queue:" + queue1.Contains("MCA"));
            Console.WriteLine("The element BCA is in the queue:" + queue1.Contains("BCA"));
            Console.WriteLine("The element MTech is in the queue:" + queue1.Contains("MTech"));
        }

        static void ClearQueue()
        {
            var queue1 = new Queue<string>();
            queue1.Enqueue("MCA");
            queue1.Enqueue("MBA");
            queue1.Enqueue("BCA");
            queue1.Enqueue("BBA");

            Console.WriteLine("The elements in the queue are:" + queue1.Count);
            
            queue1.Clear();
            
            Console.WriteLine("The elements in the queue are after the clear method:" + queue1.Count);
        }
    }
}