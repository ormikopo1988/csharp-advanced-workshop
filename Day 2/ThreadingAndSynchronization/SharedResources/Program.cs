using System;
using System.Threading;

namespace SharedResources
{
    class Program
    {
        private static bool isCompleted = false;

        private static readonly object lockCompleted = new object();

        static void Main(string[] args)
        {
            // I want to print the message inside HelloWorld method only once
            Thread thread = new Thread(HelloWorld);

            // Worker Thread
            thread.Start();
            
            // Main Thread
            HelloWorld();

            Console.ReadLine();
        }

        // What is the problem with this code?
        private static void HelloWorld()
        {
            if (!isCompleted)
            {
                Console.WriteLine("Hello world should print only once.");

                isCompleted = true;
            }
        }

        //private static void HelloWorld()
        //{
        //    // Let's say the worker comes here first.
        //    // Every other thread that comes here after worker thread has acquired the lock 
        //    // will have to wait until that particular thread, which is the worker thread in this case, 
        //    // is able to use this, and execute the following lines
        //    lock (lockCompleted)
        //    {
        //        if (!isCompleted)
        //        {
        //            Console.WriteLine("Hello world should print only once.");

        //            isCompleted = true;
        //        }
        //    }
        //}
    }
}
