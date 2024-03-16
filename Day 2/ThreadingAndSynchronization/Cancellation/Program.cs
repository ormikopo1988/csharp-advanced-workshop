using Cancellation;

Console.WriteLine("Starting application.");

var source = new CancellationTokenSource();

// Assuming the wrapping class is CancellableTaskTest
var task = CancellableTaskTest.CreateCancellableTask(source.Token);

Console.WriteLine("Heavy process invoked.");

Console.WriteLine("Press C to cancel.");
Console.WriteLine("");

char ch = Console.ReadKey().KeyChar;

if (ch == 'c' || ch == 'C')
{
    source.Cancel();

    Console.WriteLine("\nTask cancellation requested.");
}

try
{
    task.Wait();
}
catch (AggregateException ex)
{
    if (ex.InnerExceptions.Any(e => e is TaskCanceledException))
    {
        Console.WriteLine("Task cancelled exception detected.");
    }
    else
    {
        throw;
    }
}
catch (Exception)
{
    throw;
}
finally
{
    source.Dispose();
}

Console.WriteLine("Process completed.");

Console.ReadLine();