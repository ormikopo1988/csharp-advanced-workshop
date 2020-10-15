using System;

namespace CreatingGenerics
{
    public static class Extensions
    {
        public static void Display<T>(this CustomLinkedList<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}