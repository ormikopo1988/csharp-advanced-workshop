using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IOBoundOperation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} starts execution of Main.");

            var resultTask = GetFirstCharactersCountAsync("http://www.dotnetfoundation.org", 10);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Doing random work on thread {Thread.CurrentThread.ManagedThreadId} in Main.");
            }

            var result = await resultTask;

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} result in Main: {result}");

            Console.ReadLine();
        }

        private static async Task<string> GetFirstCharactersCountAsync(string url, int count)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} starts execution of GetFirstCharactersCountAsync.");

            // Execution is synchronous here
            var client = new HttpClient();

            var pageTask = client.GetStringAsync(url);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Doing random work on thread {Thread.CurrentThread.ManagedThreadId} in GetFirstCharactersCountAsync.");
            }

            // Execution of GetFirstCharactersCountAsync() is yielded to the caller here
            // GetStringAsync returns a Task<string>, which is *awaited*
            var page = await pageTask;

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} resumes execution of GetFirstCharactersCountAsync when the client.GetStringAsync task completes.");

            // Execution resumes when the client.GetStringAsync task completes,
            // becoming synchronous again.
            if (count > page.Length)
            {
                return page;
            }
            else
            {
                return page.Substring(0, count);
            }
        }
    }
}
