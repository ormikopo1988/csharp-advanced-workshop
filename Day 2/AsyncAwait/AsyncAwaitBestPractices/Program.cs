using BenchmarkDotNet.Running;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitBestPractices
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region Avoid async void

            // Bad
            //ThisWillNotCatchTheException();
            // Good
            //await ThisWillCatchTheExceptionAsync();

            #endregion

            #region Do not wrap return Task inside try..catch{} or using{} block

            // Bad
            //Console.WriteLine(await ReturnTaskExceptionNotCaughtAsync());
            // Good
            //Console.WriteLine(await ReturnTaskExceptionCaughtAsync());

            #endregion

            #region Cancellation of async operation

            //var cts = new CancellationTokenSource();

            //var thread = new Thread(() =>
            //{
            //    if (Console.ReadKey().KeyChar == 'x')
            //    {
            //        cts.Cancel();
            //    }
            //});
            //thread.Start();

            //try
            //{
            //    await MockMethodAsync(cts.Token);
            //}
            //catch (OperationCanceledException ex)
            //{
            //    Console.WriteLine(ex);
            //}

            #endregion

            #region Awaiting completion of multiple async Tasks

            //var ghSrv = new GitHubService();

            //var gitHubUserInfosTask = Task.WhenAll(
            //    ghSrv.GetGitHubUserInfoAsyncTask("ormikopo1988"),
            //    ghSrv.GetGitHubUserInfoAsyncTask("demo"),
            //    ghSrv.GetGitHubUserInfoAsyncTask("doe"),
            //    ghSrv.GetGitHubUserInfoAsyncTask("string")
            //);

            //for (var i = 0; i < 10000; i++) 
            //{
            //    Console.WriteLine("Doing some synchronous work.");
            //}

            //var gitHubUserInfos = await gitHubUserInfosTask;

            //foreach (var gitHubUserInfo in gitHubUserInfos)
            //{
            //    if (gitHubUserInfo is not null)
            //    {
            //        Console.WriteLine($"{gitHubUserInfo.Name} [{gitHubUserInfo.Username}]: {gitHubUserInfo.ProfileUrl}");
            //    }
            //}

            #endregion

            #region Utilize ValueTask and ValueTask<T>

            // Remember to change the Configuration Manager to "Release" mode to run the benchmark.
            //BenchmarkRunner.Run<GitHubServiceBenchmarks>();

            #endregion

            Console.ReadLine();
        }

        #region Avoid async void

        static void ThisWillNotCatchTheException()
        {
            try
            {
                VoidMethodThrowsExceptionAsync();
            }
            catch (Exception ex)
            {
                //The below line will never be reached
                Console.WriteLine(ex);
            }
        }

        static async void VoidMethodThrowsExceptionAsync()
        {
            await Task.Delay(100);

            throw new Exception("Hmmm, something went wrong!");
        }

        static async Task ThisWillCatchTheExceptionAsync()
        {
            try
            {
                await TaskMethodThrowsExceptionAsync();
            }
            catch (Exception ex)
            {
                //The below line will actually be reached
                Console.WriteLine(ex);
            }
        }

        static async Task TaskMethodThrowsExceptionAsync()
        {
            await Task.Delay(100);

            throw new Exception("Hmmm, something went wrong!");
        }

        #endregion

        #region Do not wrap return Task inside try..catch{} or using{} block

        static Task<string> ReturnTaskExceptionNotCaughtAsync()
        {
            try
            {
                //Bad idea...
                return GetDataThrowsExceptionAsync();
            }
            catch (Exception ex)
            {
                //The below line will never be reached
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        static async Task<string> ReturnTaskExceptionCaughtAsync()
        {
            try
            {
                //Good idea...
                return await GetDataThrowsExceptionAsync();
            }
            catch (Exception ex)
            {
                //The below line will be reached
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        static async Task<string> GetDataThrowsExceptionAsync()
        {
            await Task.Delay(100);
            throw null;
        }

        #endregion

        #region Cancellation of async operation

        static async Task MockMethodAsync(CancellationToken ct)
        {
            for (var i = 0; i < 100; i++)
            {
                //Simulate an async call that takes some time to complete
                await Task.Delay(1000, ct);

                Console.WriteLine("Doing some async operation...");

                //Check if cancellation has been requested
                ct.ThrowIfCancellationRequested();
            }
        }

        #endregion
    }
}