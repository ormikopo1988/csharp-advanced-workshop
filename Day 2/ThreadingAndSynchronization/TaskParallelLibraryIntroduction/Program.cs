using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Diagnostics;

var stopwatch = new Stopwatch();

stopwatch.Start();
SequentialFor();
stopwatch.Stop();

Console.WriteLine($"Time taken for SequentialFor: {stopwatch.ElapsedMilliseconds}");

stopwatch.Reset();

stopwatch.Start();
ParallelFor();
stopwatch.Stop();

Console.WriteLine($"Time taken for ParallelFor: {stopwatch.ElapsedMilliseconds}");

stopwatch.Reset();

stopwatch.Start();
ParallelForEach();
stopwatch.Stop();

Console.WriteLine($"Time taken for ParallelForEach: {stopwatch.ElapsedMilliseconds}");

stopwatch.Reset();

stopwatch.Start();
ParallelInvoke();
stopwatch.Stop();

Console.WriteLine($"Time taken for ParallelInvoke: {stopwatch.ElapsedMilliseconds}");

// All the above simple examples generally take much lesser time
// that general sequential approach (e.g. just above 1 second rather than 5 seconds).

Console.ReadLine();

// Demo Sequential Method
static void SequentialFor()
{
    for (var i = 1; i < 6; i++)
    {
        SlowTyper(i);
    }
}

// Demo TPL Methods
static void ParallelFor()
{
    var k = Parallel.For(1, 6,
        (idx) => SlowTyper(idx));
}

static void ParallelForEach()
{
    Parallel.ForEach(Enumerable.Range(1, 5),
        (idx) => SlowTyper(idx));
}

static void ParallelInvoke()
{
    Parallel.Invoke(() => SlowTyper(1), () => SlowTyper(2),
        () => SlowTyper(3), () => SlowTyper(4), () => SlowTyper(5));
}

// A slow method that takes a second to write the value
static void SlowTyper(int value)
{
    Thread.Sleep(1000);
    Console.WriteLine($"Value: {value}");
}