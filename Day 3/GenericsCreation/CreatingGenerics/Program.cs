using System;

namespace CreatingGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            //CustomLinkedListDemo();
            //GenericMethodsDemo();
            GenericStaticMethodsDemo();
        }

        static void CustomLinkedListDemo()
        {
            var list = new CustomLinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(2);
            list.AddLast(3);
            list.Remove(2);
            
            var sum = 0;

            foreach (var item in list)
            {
                sum += item;
            }

            Console.WriteLine($"Sum of elements in linked list is: {sum}");
        }

        static void GenericMethodsDemo()
        {
            // Uncomment this code for C# Generic Method demo
            var list = new CustomLinkedList<double>();
            list.AddLast(1.5);
            list.AddLast(2.2);
            list.AddLast(2.6);
            list.AddLast(3.3);
            list.Remove(2.2);

            var sum = 0D;

            foreach (var item in list.AsEnumerableOf<int>())
            {
                sum += item;
                Console.WriteLine(item);
            }

            Console.WriteLine($"Sum of elements in linked list is: {sum}");
        }

        static void GenericStaticMethodsDemo()
        {
            var list = new CustomLinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(2);
            list.AddLast(3);
            list.Remove(2);

            list.Display();
        }
    }
}