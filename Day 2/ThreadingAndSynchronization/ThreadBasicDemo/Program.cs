using System.Threading;
using System;

Thread.CurrentThread.Name = "Main Thread";

Console.WriteLine($"Main starts execution in {Thread.CurrentThread.Name}.");

// A very basic example of running a simple code on a new thread
DoWorkOnThread();

Console.WriteLine("=====================================================");

// A Thread is not a method, so it does not return a value. 
// But if we want a result back after a thread has completed its work,
// there are few ways of doing it. Following code shows an example using a closure
ValueReturningThread();

Console.ReadLine();

static void SlowMethod()
{
    Console.WriteLine($"{nameof(SlowMethod)} starts execution in {Thread.CurrentThread.Name}.");

    Thread.Sleep(1500);

    Console.WriteLine($"{nameof(SlowMethod)} Work completed in {Thread.CurrentThread.Name}.");
}

static void DoWorkOnThread()
{
    Console.WriteLine($"{nameof(DoWorkOnThread)} starts execution in {Thread.CurrentThread.Name}.");

    var thread = new Thread(SlowMethod)
    {
        Name = "Simple Worker Thread"
    };

    // Starts work on new thread
    thread.Start();

    // Continue working on current thread
    Console.WriteLine($"{nameof(DoWorkOnThread)} continues working in {Thread.CurrentThread.Name}.");
}

static void ValueReturningThread()
{
    Console.WriteLine($"{nameof(ValueReturningThread)} starts execution in {Thread.CurrentThread.Name}.");

    var result = string.Empty;

    Thread thread = new(() =>
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} starts executing its work to return a value with a closure.");
        Thread.Sleep(5000);
        result = $"\"{Thread.CurrentThread.Name} work completed\"";
    })
    {
        Name = "Value Returning Worker Thread"
    };

    thread.Start();

    // Do other stuff
    Console.WriteLine($"{nameof(ValueReturningThread)} does other stuff in {Thread.CurrentThread.Name}.");

    // Block the main thread here and wait for the worker thread to terminate
    thread.Join();

    Console.WriteLine($"{nameof(ValueReturningThread)} returns value: {result} in {Thread.CurrentThread.Name}.");
}