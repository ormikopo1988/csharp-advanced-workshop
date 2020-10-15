using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncStreamsBasic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Basic demo 1
                await PrintNumbersAsync();

                // Basic demo 2
                //YieldReturnMain();
                //await AwaitAndYieldReturnMainAsync();

                // Use case demo
                //await PagingApiMainAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Done.");
        }

        #region Basic Demo 1

        static async Task PrintNumbersAsync()
        {
            await foreach (var number in GenerateNumberSequenceAsync())
            {
                Console.WriteLine($"The async stream generated: {number}");
            }
        }

        static async IAsyncEnumerable<int> GenerateNumberSequenceAsync()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Computing a value in 100ms and return it asynchronously...");
                await Task.Delay(100);
                yield return i;
            }
        }

        #endregion

        #region Basic demo 2

        static void YieldReturnMain()
        {
            foreach (int item in YieldReturn())
            {
                Console.WriteLine($"Got {item} from thread {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        static IEnumerable<int> YieldReturn()
        {
            // Deferred execution!
            yield return 1;
            yield return 2;
            yield return 3;
        }

        static async Task AwaitAndYieldReturnMainAsync()
        {
            await foreach (int item in AwaitAndYieldReturnAsync())
            {
                Console.WriteLine($"Got {item} from thread {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        static async IAsyncEnumerable<int> AwaitAndYieldReturnAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1)); // pause (await)
            yield return 1; // pause (produce value)
            await Task.Delay(TimeSpan.FromSeconds(1)); // pause (await)
            yield return 2; // pause (produce value)
            await Task.Delay(TimeSpan.FromSeconds(1)); // pause (await)
            yield return 3; // pause (produce value)
        }

        #endregion

        #region Use case demo

        static async Task PagingApiMainAsync()
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss} Starting...");

            await foreach (int item in GetPagingApiDataAsync())
            {
                Console.WriteLine($"{DateTime.Now:hh:mm:ss} Got {item}");
            }
        }

        static async IAsyncEnumerable<int> GetPagingApiDataAsync()
        {
            // Handle the paging only in this function.
            // Other functions don't get polluted with paging logic.
            const int pageSize = 5;
            int offset = 0;

            while (true)
            {
                // Get next page of results.
                string jsonString = await HttpClient.GetStringAsync(
                    $"https://localhost:44391/api/values?offset={offset}&limit={pageSize}");

                // Produce them for our consumer.
                int[] results = JsonConvert.DeserializeObject<int[]>(jsonString);

                foreach (int result in results)
                {
                    yield return result;
                }

                // If this is the last page, then stop.
                if (results.Length != pageSize)
                {
                    break;
                }

                // Index to the next page.
                offset += pageSize;
            }
        }

        static readonly HttpClient HttpClient = new HttpClient();

        #endregion
    }
}