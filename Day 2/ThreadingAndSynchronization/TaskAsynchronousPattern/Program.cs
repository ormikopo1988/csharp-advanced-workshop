using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsynchronousPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();

            var token = tokenSource.Token;

            Task.Factory.StartNew(() =>
            {
                // Wait 4 seconds and then stop the downloading
                Thread.Sleep(4000);

                tokenSource.Cancel();
            });

            DownloadAsync(new Uri("https://jsonplaceholder.typicode.com/posts"), token);

            Console.ReadLine();
        }

        static async Task DownloadAsync(Uri uri, CancellationToken cancellationToken)
        {
            // Stop only if the operation is cancelled explicitly through the CancenllationToken
            while(true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    HttpResponseMessage response = await GetAsync(uri, cancellationToken);

                    Console.WriteLine($"The response is: {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static async Task<HttpResponseMessage> GetAsync(Uri uri, CancellationToken cancellationToken)
        {
            using(var httpClient = new HttpClient())
            {
                return await httpClient.GetAsync(uri, cancellationToken);
            }
        }
    }
}
