using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IOBoundOperation
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} starts execution of {nameof(Main)}.");

            var resultTask = GetFirstCharactersAsync("http://www.dotnetfoundation.org", 10);

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine($"Doing random work on thread {Environment.CurrentManagedThreadId} in {nameof(Main)}.");
            }

            var result = await resultTask;

            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} result in {nameof(Main)}: {result}");

            Console.ReadLine();
        }

        private static async Task<string> GetFirstCharactersAsync(string url, int count)
        {
            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} starts execution of {nameof(GetFirstCharactersAsync)}.");

            // Execution is synchronous here
            using var client = new HttpClient();

            var pageTask = client.GetStringAsync(url);

            // Try to change here the value from 10 -> 10000 to see how the await pageTask 
            // below will run synchronously as the Task will have already been completed.
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine($"Doing random work on thread {Environment.CurrentManagedThreadId} in {nameof(GetFirstCharactersAsync)}.");
            }

            // Execution of GetFirstCharactersAsync() is yielded to the caller here
            // GetStringAsync returns a Task<string>, which is *awaited*
            var page = await pageTask;

            Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} resumes execution of {nameof(GetFirstCharactersAsync)} when the client.GetStringAsync task completes.");

            // Execution resumes when the client.GetStringAsync task completes,
            // becoming synchronous again.
            return count > page.Length ?
                page : page[..count];
        }
    }
}