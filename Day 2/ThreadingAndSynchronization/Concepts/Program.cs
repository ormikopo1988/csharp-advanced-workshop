using System;
using System.Threading;

namespace Concepts
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(PrintHelloWorld);

            // A worker thread starts, prints Hello World and then sleeps for 5 seconds
            thread.Start();

            // We could also mark the thread as a background one
            thread.IsBackground = true;

            // If we wanted to wait for the thread to finish before printing the below we could do:
            // Both Join() and Sleep() would block the thread
            thread.Join();

            Console.WriteLine("Hello world from main thread.");

            Console.ReadKey();
        }

        private static void PrintHelloWorld()
        {
            Console.WriteLine("Hello World from worker thread.");

            // Let's imagine that this process took 5 seconds to complete
            Thread.Sleep(5000);
        }
    }
}
