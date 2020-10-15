using System;
using System.Threading;

namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo();

            Console.ReadLine();
        }

        private static void Demo()
        {
            // This will not catch it because exception handling is per thread
            try
            {
                // Worker Thread
                new Thread(Execute).Start();
            }
            // This catch happens on the main thread
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Execute()
        {
            throw null;
        }

        //private static void Execute()
        //{
        //    try
        //    {
        //        // This runs on the worker thread
        //        throw null;
        //    }
        //    // This catch happens on the worker thread
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}
