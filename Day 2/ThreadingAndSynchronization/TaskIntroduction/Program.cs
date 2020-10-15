using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskIntroduction
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Main starts execution on Thread {Thread.CurrentThread.ManagedThreadId}");

            // Option 1: new Task(Action).Start();
            // Task that does not return a value
            Task task = new Task(SimpleMethod);
            task.Start();

            Console.WriteLine($"Main continues execution on Thread {Thread.CurrentThread.ManagedThreadId} after starting SimpleMethod task.");

            // Task that returns a value
            Task<string> taskThatReturnsValue = new Task<string>(MethodThatReturnsValue);
            taskThatReturnsValue.Start();

            Console.WriteLine($"Main continues execution on Thread {Thread.CurrentThread.ManagedThreadId} after starting MethodThatReturnsValue task - Option 1.");

            // Block the current thread until the Task is completed
            taskThatReturnsValue.Wait();

            // Get the result from the Task operation - Blocking operation on current thread
            Console.WriteLine(taskThatReturnsValue.Result);

            // Option 2: Task.Factory.StartNew(Action);
            var cancellationTokenSource = new CancellationTokenSource();
            var task2 = Task.Factory.StartNew(() => MethodThatReturnsValue(),
                cancellationTokenSource.Token,
                TaskCreationOptions.LongRunning, //this
                TaskScheduler.Default);

            // Execution can continue from here on original thread
            Console.WriteLine($"Main continues execution on Thread {Thread.CurrentThread.ManagedThreadId} after starting MethodThatReturnsValue task - Option 2.");

            // Get the result from the Task operation - Blocking operation on current thread
            Console.WriteLine(taskThatReturnsValue.Result);

            // Option 3: Task.Factory.StartNew(Action);
            // Will run on separate thread
            var task3 = Task.Run(() => MethodThatReturnsValue());

            // Execution can continue from here on original thread
            Console.WriteLine($"Main continues execution on Thread {Thread.CurrentThread.ManagedThreadId} after starting MethodThatReturnsValue task - Option 3.");

            // Get the result from the Task operation - Blocking operation on current thread
            Console.WriteLine(taskThatReturnsValue.Result);

            Console.ReadLine();
        }

        private static void SimpleMethod()
        {
            Console.WriteLine($"Hello from SimpleMethod on {Thread.CurrentThread.ManagedThreadId}.");
        }

        private static string MethodThatReturnsValue()
        {
            // This simulates a computational intensive operation
            Thread.Sleep(2000);

            return $"Hello from MethodThatReturnsValue on {Thread.CurrentThread.ManagedThreadId}.";
        }
    }
}
