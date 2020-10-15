using System;
using System.Collections.Generic;

namespace GenericTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            var l1 = new List<string>();
            var l2 = new List<string>();
            var l3 = new List<int>();
            var l4 = new List<object>();

            Console.WriteLine(l1.GetType());
            Console.WriteLine(l2.GetType());
            Console.WriteLine(l3.GetType());
            Console.WriteLine(l4.GetType());

            Console.WriteLine(l1.GetType() == l2.GetType());
            Console.WriteLine(l1.GetType() == l3.GetType());
            Console.WriteLine(l1.GetType() == l4.GetType());
        }
    }
}