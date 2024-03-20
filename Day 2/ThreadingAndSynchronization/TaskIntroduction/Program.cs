Console.WriteLine($"Main starts execution on Thread {Environment.CurrentManagedThreadId}.");

// Option 1: new Task(Action).Start();
// Task that does not return a value
var task = new Task(SimpleMethod);
task.Start();

Console.WriteLine($"Main continues execution on Thread {Environment.CurrentManagedThreadId} after starting {nameof(SimpleMethod)} task.");

// Task that returns a value
var taskThatReturnsValue = new Task<string>(MethodThatReturnsValue);
taskThatReturnsValue.Start();

Console.WriteLine($"Main continues execution on Thread {Environment.CurrentManagedThreadId} after starting {nameof(MethodThatReturnsValue)} task - Option 1.");

// Block the current thread until the Task is completed
taskThatReturnsValue.Wait();

// Get the result from the Task operation - Blocking operation on current thread
Console.WriteLine(taskThatReturnsValue.Result);

// Option 2: Task.Factory.StartNew(Action);
var cancellationTokenSource = new CancellationTokenSource();
var task2 = Task.Factory.StartNew(() => MethodThatReturnsValue(),
    cancellationTokenSource.Token,
    TaskCreationOptions.LongRunning,
    TaskScheduler.Default);

// Execution can continue from here on original thread
Console.WriteLine($"Main continues execution on Thread {Environment.CurrentManagedThreadId} after starting {nameof(MethodThatReturnsValue)} task - Option 2.");

// Get the result from the Task operation - Blocking operation on current thread
Console.WriteLine(task2.Result);

// Option 3: Task.Run(Action);
// Will run on separate thread
var task3 = Task.Run(() => MethodThatReturnsValue());

// Execution can continue from here on original thread
Console.WriteLine($"Main continues execution on Thread {Environment.CurrentManagedThreadId} after starting {nameof(MethodThatReturnsValue)} task - Option 3.");

// Get the result from the Task operation - Blocking operation on current thread
Console.WriteLine(task3.Result);

Console.ReadLine();

static void SimpleMethod()
{
    Console.WriteLine($"Hello from {nameof(SimpleMethod)} on Thread {Environment.CurrentManagedThreadId}.");
}

static string MethodThatReturnsValue()
{
    // This simulates a computational intensive operation
    Thread.Sleep(2000);

    return $"Hello from {nameof(MethodThatReturnsValue)} on Thread {Environment.CurrentManagedThreadId}.";
}