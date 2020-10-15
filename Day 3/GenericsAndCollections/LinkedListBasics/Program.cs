using System;
using System.Collections.Generic;

namespace LinkedListBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            AddItemsAndWaysToIterateThroughLinkedList();
            RemoveItemsFromLinkedList();
            FindItemsInLinkedList();
        }

        static void AddItemsAndWaysToIterateThroughLinkedList()
        {
            var linkedList = new LinkedList<int>();

            linkedList.AddFirst(2);
            linkedList.AddFirst(3);

            var first = linkedList.First; // this is of LinkedListNode<int> type

            linkedList.AddAfter(first, 5);
            linkedList.AddBefore(first, 10);

            // 1st way to iterate
            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }

            // 2nd way to iterate
            var node = linkedList.First;

            while(node != null)
            {
                Console.WriteLine(node.Value);

                node = node.Next;
            }
        }

        static void RemoveItemsFromLinkedList()
        {
            var linkedList = new LinkedList<string>();
            linkedList.AddFirst("John");
            linkedList.AddLast("Doe");
            linkedList.RemoveLast();

            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }
        }

        static void FindItemsInLinkedList()
        {
            var linkedList = new LinkedList<string>();
            linkedList.AddFirst("John");
            linkedList.AddLast("Doe");
            linkedList.RemoveLast();

            Console.WriteLine(linkedList.Contains("John"));
            Console.WriteLine(linkedList.Contains("Scott"));
        }
    }
}