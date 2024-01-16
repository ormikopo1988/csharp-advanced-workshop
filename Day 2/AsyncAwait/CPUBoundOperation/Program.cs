using System;
using System.Threading;
using System.Threading.Tasks;

namespace CPUBoundOperation
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} starts execution of {nameof(Main)}.");

            var totalAfterTaxTask = CalculateTotalAfterTaxAsync(70); // Start the work

            DoSomethingSynchronous(); // Start other work as well

            // Now suspend execution of Main and wait for the task to complete asynchronously
            var totalAfterTaxResult = await totalAfterTaxTask;

            // THIS MAY RUN ON A SEPARATE THREAD
            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} result in {nameof(Main)}: {totalAfterTaxResult}");

            Console.ReadLine();
        }

        private static void DoSomethingSynchronous()
        {
            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} starts execution of {nameof(DoSomethingSynchronous)}.");
        }

        private static async Task<decimal> CalculateTotalAfterTaxAsync(decimal value)
        {
            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} starts execution of {nameof(CalculateTotalAfterTaxAsync)}.");

            var totalAfterTaxTask = Task.Run(() =>
            {
                // THIS RUNS ON A SEPARATE THREAD
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} started CPU Bound asynchronous task on a background thread inside {nameof(CalculateTotalAfterTaxAsync)}.");

                Thread.Sleep(3000); // Works for 3 seconds and calculates a result to return

                return value * 1.2M;
            });

            for (var i = 0; i < 3; i++)
            {
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} works on other independent work simultaneously.");
            }

            // Now suspend execution of CalculateTotalAfterTaxAsync, yield control to the caller 
            // of CalculateTotalAfterTaxAsync and wait for the task to complete asynchronously
            var result = await totalAfterTaxTask;

            // THIS MAY RUN ON A SEPARATE THREAD
            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} continues execution of {nameof(CalculateTotalAfterTaxAsync)} after getting the result of the CPU bound task.");

            return result;
        }
    }
}