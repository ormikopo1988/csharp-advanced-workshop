var task = Task.Factory.StartNew<string>(() => GetPosts("https://jsonplaceholder.typicode.com/posts"));

// Here we explicitly do not block the main thread from executing SomethingElse()
SomethingElse();

try
{
    // When you type .Wait() or .Result on the task object
    // you say that you want to wait for the result to come before continuing.
    //task.Wait();
    Console.WriteLine(task.Result);
}
// Every time we try to access the .Result from a task it can throw an AggregateException
catch (AggregateException ex)
{
    Console.Error.WriteLine(ex.Message);
}

Console.ReadLine();

static void SomethingElse()
{
    Console.WriteLine("Some other dummy operation happening in main thread.");
}

static string GetPosts(string url)
{
    // Simulate exception
    //throw null;

    using var client = new HttpClient();

    return client.GetStringAsync(url).Result;
}