Task<string> antecedent = Task.Run(() => {
    // Simulate a long running task
    Task.Delay(2000).Wait();

    return DateTime.Today.ToShortDateString();
});

// We want here to pass the antecedent data to the continuation
// The `t` argument is same as `task`
Task<string> continuation = antecedent.ContinueWith(t => {
    return "Today is " + t.Result;
});

// Method execution will continue here normally
Console.WriteLine("This will display before the result.");

// Note: Using continuation.Result makes the process synchronous, 
// execution WILL WAIT here on current thread for the task to complete
Console.WriteLine(continuation.Result);

Console.ReadLine();