using System;
using System.Threading;

namespace Semaphores
{
    public class Program
    {
        // Only allow 3 concurrent threads to access a certail resource
        private static readonly SemaphoreSlim semaphoreSlim = new(3);

        public static void Main()
        {
            for (var i = 0; i < 10; i++)
            {
                // Simulating the try to enter the resource block
                // that the semaphore is trying to protect.
                new Thread(EnterSemaphore).Start();
            }

            Console.ReadLine();
        }

        private static void EnterSemaphore()
        {
            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} is waiting to be part of the club.");

            semaphoreSlim.Wait();

            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} is part of the club.");
            Thread.Sleep(2000); // Simulate some operation that needs synchronization

            semaphoreSlim.Release();

            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} left the club.");
        }
    }
}