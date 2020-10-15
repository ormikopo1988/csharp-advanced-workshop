using System;
using System.Threading;

namespace LocalMemory
{
    class Program
    {
        static void Main(string[] args)
        {
            // Worker Thread
            var workerThread = new Thread(PrintOneToThirty);
            workerThread.Name = "WorkerTh";
            workerThread.Start();

            // Main Thread
            Thread.CurrentThread.Name = "MainTh";
            PrintOneToThirty();

            Console.ReadLine();
        }

        private static void PrintOneToThirty()
        {
            // This variable i will be part of the local memory allocated for each thread
            for(int i=0; i<30; i++)
            {
                Console.Write($"{Thread.CurrentThread.Name}: {i+1} ");
            }
        }
    }
}
