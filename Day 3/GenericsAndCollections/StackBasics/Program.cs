using System;
using System.Collections.Generic;

namespace StackBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            StackBasicOperations();
            StackContainOperation();
            ClearStack();
        }

        static void StackBasicOperations()
        {
            var stack = new Stack<Employee>();

            stack.Push(new Employee
            {
                Name = "John"
            });
            stack.Push(new Employee
            {
                Name = "Jane"
            });
            stack.Push(new Employee
            {
                Name = "Scott"
            });

            var firstEmpInStack = stack.Peek();

            Console.WriteLine($"First employee in the stack is: {firstEmpInStack.Name}");

            while (stack.Count > 0)
            {
                var employee = stack.Pop();

                Console.WriteLine(employee.Name);
            }
        }

        static void StackContainOperation()
        {
            var stack1 = new Stack<string>();
            stack1.Push("MCA");
            stack1.Push("MBA");
            stack1.Push("BCA");
            stack1.Push("BBA");

            Console.WriteLine("The elements in the Stack are:");

            foreach (string s in stack1)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("The element MCA is in the Stack:" + stack1.Contains("MCA"));
            Console.WriteLine("The element BCA is in the Stack:" + stack1.Contains("BCA"));
            Console.WriteLine("The element MTech is in the Stack:" + stack1.Contains("MTech"));
        }

        static void ClearStack()
        {
            var stack1 = new Stack<string>();
            stack1.Push("MCA");
            stack1.Push("MBA");
            stack1.Push("BCA");
            stack1.Push("BBA");

            Console.WriteLine("The elements in the Stack are:" + stack1.Count);

            stack1.Clear();

            Console.WriteLine("The elements in the Stack are after the clear method:" + stack1.Count);
        }
    }
}