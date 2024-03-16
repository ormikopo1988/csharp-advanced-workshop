// Worker Thread
var workerThread = new Thread(PrintOneToOneHundred)
{
    Name = "WT"
};
workerThread.Start();

// Main Thread
Thread.CurrentThread.Name = "MT";
PrintOneToOneHundred();

Console.ReadLine();

static void PrintOneToOneHundred()
{
    // This variable i will be part of the local memory allocated for each thread
    for (var i = 0; i < 100; i++)
    {
        Console.Write($"{Thread.CurrentThread.Name}: {i + 1} ");
    }
}