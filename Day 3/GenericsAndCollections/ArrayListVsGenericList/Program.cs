using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ArrayListVsGenericList
{
    class Program
    {
        static void Main(string[] args)
        {
            // The first thing you can notice here is that sorting the generic list is significantly faster than sorting the array list (non-generic list). 
            // Because the runtime knows the generic List<int> is of type Int32, it can store the list elements in an underlying integer array in memory, 
            // while the non-generic ArrayList has to cast each list element to an object. 
            // As this example shows, the extra casts take up time and slow down the list sort.

            //generic list sort
            GenericListSort();

            //non-generic list sort
            NonGenericListSort();
        }

        static void GenericListSort()
        {
            var listGeneric = new List<int> { 5, 9, 1, 4 };

            // timer for generic list sort
            var s = Stopwatch.StartNew();

            listGeneric.Sort();

            s.Stop();

            Console.WriteLine($"Generic Sort: {listGeneric}  \n Time taken: {s.Elapsed.TotalMilliseconds}ms");
        }

        static void NonGenericListSort()
        {
            var listNonGeneric = new ArrayList { 5, 9, 1, 4 };

            //timer for non-generic list sort
            var s2 = Stopwatch.StartNew();

            listNonGeneric.Sort();

            s2.Stop();

            Console.WriteLine($"Non-Generic Sort: {listNonGeneric}  \n Time taken: {s2.Elapsed.TotalMilliseconds}ms");
        }
    }
}