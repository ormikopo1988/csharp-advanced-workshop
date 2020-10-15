using System;
using System.Threading;

namespace ThreadBasicDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main Thread";

            Console.WriteLine($"Main starts execution in {Thread.CurrentThread.Name}");

            // A very basic example of running a simple code on a new thread
            DoWorkOnThread();

            Console.WriteLine("=====================================================");

            // A Thread is not a method, so it does not return a value. 
            // But if we want a result back after a thread has completed its work, there are few ways of doing it. Following code shows an example using a closure
            ValueReturningThread();
        }

        static void SlowMethod()
        {
            Console.WriteLine($"SlowMethod starts execution in {Thread.CurrentThread.Name}");

            Thread.Sleep(1500);
            
            Console.WriteLine($"SlowMethod Work completed in {Thread.CurrentThread.Name}");
        }

        static void DoWorkOnThread()
        {
            Console.WriteLine($"DoWorkOnThread starts execution in {Thread.CurrentThread.Name}");

            var thread = new Thread(SlowMethod);
            thread.Name = "Simple Worker Thread";

            // Starts work on new thread
            thread.Start();

            // Continue working on current thread
            Console.WriteLine($"DoWorkOnThread continues working in {Thread.CurrentThread.Name}");
        }

        static void ValueReturningThread()
        {
            Console.WriteLine($"ValueReturningThread starts execution in {Thread.CurrentThread.Name}");

            string result = null;

            Thread thread = new Thread(() => {
                Console.WriteLine($"{Thread.CurrentThread.Name} starts executing its work to return a value with a closure.");
                Thread.Sleep(5000);
                result = $"{Thread.CurrentThread.Name} work completed.";
            });
            thread.Name = "Value Returning Worker Thread";
            
            thread.Start();

            // Do other stuff
            Console.WriteLine($"ValueReturningThread does other stuff {Thread.CurrentThread.Name}");

            // Block the main thread here and wait for the worker thread to terminate
            thread.Join();

            Console.WriteLine($"ValueReturningThread returns value: {result} on {Thread.CurrentThread.Name}");
        }
    }
}
