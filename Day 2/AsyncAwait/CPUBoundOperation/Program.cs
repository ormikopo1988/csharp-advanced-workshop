using System;
using System.Threading;
using System.Threading.Tasks;

namespace CPUBoundOperation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} starts execution of Main.");

            var totalAfterTaxTask = CalculateTotalAfterTaxAsync(70); // Start the work

            DoSomethingSynchronous(); // Start other work as well

            // Now suspend execution of Main and wait for the task to complete asynchronously
            var totalAfterTaxResult = await totalAfterTaxTask;

            // THIS MAY RUN ON A SEPARATE THREAD
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} result in Main: {totalAfterTaxResult}");

            Console.ReadLine();
        }

        private static void DoSomethingSynchronous()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} starts execution of DoSomethingSynchronous.");
        }

        private static async Task<float> CalculateTotalAfterTaxAsync(float value)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} starts execution of CalculateTotalAfterTaxAsync.");
            
            var totalAfterTaxTask = Task.Run(() => 
            {
                // THIS RUNS ON A SEPARATE THREAD
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started CPU Bound asynchronous task on a background thread inside CalculateTotalAfterTaxAsync.");

                Thread.Sleep(3000); // Works for 3 seconds and calculates a result to return

                return value * 1.2f;
            });

            for(int i=0; i<3; i++)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} works on other independent work simultaneously.");
            }

            // Now suspend execution of CalculateTotalAfterTaxAsync, yield control to the caller 
            // of CalculateTotalAfterTaxAsync and wait for the task to complete asynchronously
            var result = await totalAfterTaxTask;

            // THIS MAY RUN ON A SEPARATE THREAD
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} continues execution of CalculateTotalAfterTaxAsync after getting the result of the CPU bound task.");

            return result;
        }
    }
}