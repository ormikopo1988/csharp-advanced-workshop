using System;
using System.Threading;
using System.Threading.Tasks;

namespace RaceCondition
{
    public class Program
    {
        private static int counter;

        public static void Main()
        {
            // Using thread
            //var t1 = new Thread(PrintStar);
            //t1.Start();

            //var t2 = new Thread(PrintPlus);
            //t2.Start();

            // Using TPL
            Task.Factory.StartNew(PrintStar);
            Task.Factory.StartNew(PrintPlus);

            Console.WriteLine("Ending main thread.");

            Console.ReadLine();
        }

        static void PrintStar()
        {
            for (counter = 0; counter < 5; counter++)
            {
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} prints: * ");
            }
        }

        static void PrintPlus()
        {
            for (counter = 0; counter < 5; counter++)
            {
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} prints: + ");
            }
        }
    }
}