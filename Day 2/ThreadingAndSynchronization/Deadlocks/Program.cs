using System;
using System.Threading;

namespace Deadlocks
{
    public class Program
    {
        private static readonly object customLock1 = new();
        private static readonly object customLock2 = new();

        public static void Main()
        {
            // Worker Thread
            new Thread(() =>
            {
                lock (customLock1)
                {
                    Console.WriteLine("Worker thread acquired custom lock 1.");
                    Thread.Sleep(2000);

                    lock (customLock2)
                    {
                        Console.WriteLine("Worker thread acquired custom lock 2.");
                    }
                }
            }).Start();

            // Main Thread
            lock (customLock2)
            {
                Console.WriteLine("Main thread acquired custom lock 2.");

                // Let's try to simulate some kind of a deadlock situation.
                // We want to make sure that custom lock 2 acquired by the main thread
                // does not get released at a time that the worker thread
                // wants it too.
                // At the same time we can create a situation here where we are making
                // the main thread contend for custom lock 1.
                Thread.Sleep(1000);
                lock (customLock1)
                {
                    Console.WriteLine("Main thread acquired custom lock 2.");
                }
            }

            Console.ReadLine();
        }
    }
}