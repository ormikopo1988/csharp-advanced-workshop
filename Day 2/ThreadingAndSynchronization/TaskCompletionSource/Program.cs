using System.Threading.Tasks;
using System.Threading;
using System;
using TaskCompletionSource;

// There are times when we want to run the task only when we get some kind of user input. 
// So, for example, if the user fires a certain event,
// at that particular event, on some kind of name change,
// we want to be able to add something in the database. 
// Something like 'task completion source class'
// is very handy to achieve something like that. 
// It will start a background task that will, later,
// complete that task once some user triggers an event that requires that task to be completed.

TaskCompletionSource<Product> taskCompletionSource = new TaskCompletionSource<Product>();

Task<Product> lazyTask = taskCompletionSource.Task;

Task.Factory.StartNew(() =>
{
    Thread.Sleep(2000);

    taskCompletionSource.SetResult(new Product
    {
        Id = 1,
        Model = "Test Product"
    });
});

Task.Factory.StartNew(() =>
{
    if (Console.ReadKey().KeyChar == 'x')
    {
        Product result = lazyTask.Result;

        Console.WriteLine("\n Result is: " + result.Model);
    }
});

Thread.Sleep(5000);

Console.ReadLine();