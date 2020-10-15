using System;
using System.Threading;

namespace Semaphores
{
    class Program
    {
        // Only allow 3 concurrent threads to access a certail resource
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                // Simulating the try to enter the resource block that the semaphore is trying to protect.
                new Thread(EnterSemaphore).Start();
            }

            Console.ReadLine();
        }

        private static void EnterSemaphore()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is waiting to be part of the club.");

            semaphoreSlim.Wait();
            
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is part of the club.");
            Thread.Sleep(2000); // Simulate some operation that needs synchronization
            
            semaphoreSlim.Release();
            
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} left the club.");
        }
    }
}
