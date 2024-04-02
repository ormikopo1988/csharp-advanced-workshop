using System.Threading;
using System;

var thread = new Thread(PrintHelloWorld)
{
    // We could also mark the thread as a background one explicitly
    IsBackground = true
};

// A worker thread starts, prints Hello World and then sleeps for 5 seconds
thread.Start();

// If we wanted to wait for the thread to finish before printing the below we could do:
// Both Join() and Sleep() would block the thread
thread.Join();

Console.WriteLine("Hello world from main thread.");

Console.ReadKey();

static void PrintHelloWorld()
{
    Console.WriteLine("Hello World from worker thread.");

    // Let's imagine that this process took 5 seconds to complete
    Thread.Sleep(5000);
}