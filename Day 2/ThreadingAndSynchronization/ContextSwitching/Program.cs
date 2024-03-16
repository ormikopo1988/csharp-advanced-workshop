// This is a worker thread
var thread = new Thread(WriteUsingNewThread)
{
    Name = "Custom Worker Thread"
};

// Starts work on new thread
// Our way of letting CLR know that it needs to talk to the Thread Scheduler 
// to spawn off a new thread
thread.Start();

// Continue working on current thread
// This is the main thread
Thread.CurrentThread.Name = "Custom Main Thread";

for (var i = 0; i < 100; i++)
{
    Console.Write($" MT: {i} ");
}

Console.ReadLine();

static void WriteUsingNewThread()
{
    for (var i = 0; i < 100; i++)
    {
        Console.Write($" WT: {i} ");
    }
}