using System;

namespace BasicGenericsExampleUse
{
    class Program
    {
        static void Main(string[] args)
        {
            var customIntList = new CustomListInt();
            customIntList.Add(12);
            customIntList.Add(2);
            customIntList.Add(23);
            customIntList.Add(9);

            for (int i = 0; i < customIntList.Length; i++)
            {
                Console.WriteLine(customIntList.Get(i));
            }

            var customStringList = new CustomListString();
            customStringList.Add("12");
            customStringList.Add("2");
            customStringList.Add("23");
            customStringList.Add("9");

            for (int i = 0; i < customStringList.Length; i++)
            {
                Console.WriteLine(customStringList.Get(i));
            }

            // What about NOT copy paste another time for doubles???
            // Better use generics for code reuse and type safety

            var list1 = new CustomList<int>();
            list1.Add(12);
            list1.Add(2);
            list1.Add(23);
            list1.Add(9);

            for (int i = 0; i < list1.Length; i++)
            {
                Console.WriteLine(list1.Get(i));
            }

            var list2 = new CustomList<string>();
            list2.Add("12");
            list2.Add("2");
            list2.Add("23");
            list2.Add("9");

            for (int i = 0; i < list2.Length; i++)
            {
                Console.WriteLine(list2.Get(i));
            }
        }
    }
}