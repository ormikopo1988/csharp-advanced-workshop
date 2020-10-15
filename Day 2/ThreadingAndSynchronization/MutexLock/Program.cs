using System;
using System.Threading;

namespace MutexLock
{
    class Program
    {
        static Mutex mutex = new Mutex(false, "CustomOsMutex");

        static void Main(string[] args)
        {
            // 10 different threads will try to acquire the mutex
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(AcquireMutex);
                thread.Name = $"Thread{i+1}";
                thread.Start();
            }

            Console.ReadLine();
        }

        private static void AcquireMutex()
        {
            // WaitOne blocks the current thread until the current instance receives a signal. It returns true when the Mutex is signaled to indicate that it is not owned
            // Try to acquire the mutex. If it is not able to acquire for the specified TimeSpan (1 second in this case) then it will not try again.
            // As a result of this code only 1 thread will be able to acquire the Mutex. All the others will step into this if statement and return.
            //if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false))
            //{
            //    Console.WriteLine($"{Thread.CurrentThread.Name} competed but was not able to acquire the Mutex within the specified TimeSpan.");
            //    return;
            //}

            // Try to acquire the mutex. Block until it will be able to. All threads will acquire the Mutex.
            //mutex.WaitOne();
            Console.WriteLine($"Mutex acquired by {Thread.CurrentThread.Name}.");
            DoSomething();
            mutex.ReleaseMutex();
            Console.WriteLine($"Mutex released by {Thread.CurrentThread.Name}.");
        }

        private static void DoSomething()
        {
            // Simulate a long running operation that needs to be protected and thread safe.
            Thread.Sleep(6000);
        } 
    }
}
