using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitBestPractices
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Avoid async void
            // Bad
            //ThisWillNotCatchTheException();
            // Good
            //await ThisWillCatchTheExceptionAsync();

            // Consider return Task instead of return await Task
            //// Good (not great)
            //Console.WriteLine(await TaskAsync());
            // Better
            //Console.WriteLine(await JustTask());

            // Do not wrap return Task inside try..catch{} or using{} block
            // Bad
            //Console.WriteLine(await ReturnTaskExceptionNotCaughtAsync());
            // Good
            //Console.WriteLine(await ReturnTaskExceptionCaughtAsync());

            // Avoid using .Wait() or .Result — Use GetAwaiter().GetResult() instead
            // Good (not great)
            //try
            //{
            //    Console.WriteLine(GetResultExample());
            //}
            //catch (AggregateException ex)
            //{
            //    Console.WriteLine(ex);
            //}
            // Better
            //try
            //{
            //    Console.WriteLine(GetAwaiterGetResultExample());
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            // Async library methods should consider using Task.ConfigureAwait(false) to boost performance
            // Good (not great)
            //Console.WriteLine(await ConfigureAwaitTrueExampleAsync());
            // Better (for library code)
            //Console.WriteLine(await ConfigureAwaitFalseExampleAsync());

            // Cancellation of async operation
            //CancellationTokenSource _cts = new CancellationTokenSource();

            //var thread = new Thread(() =>
            //{
            //    if (Console.ReadKey().KeyChar == 'x')
            //    {
            //        _cts.Cancel();
            //    }
            //});
            //thread.Start();

            //try
            //{
            //    await MockMethodAsync(_cts.Token);
            //}
            //catch (OperationCanceledException ex)
            //{
            //    Console.WriteLine(ex);
            //}
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

        #region Consider return Task instead of return await Task

        static async Task<string> TaskAsync()
        {
            //Not great!

            //...Non-async stuff happens here

            //The await is the very last line of the code path - There is no continuation after it
            return await GetDataAsync();
        }

        static Task<string> JustTask()
        {
            //Better!

            //...Non-async stuff happens here

            //Return a Task instead
            return GetDataAsync();
        }

        static async Task<string> GetDataAsync()
        {
            await Task.Delay(100);
            return "MockData";
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

        #region Avoid using .Wait() or .Result — Use GetAwaiter().GetResult() instead

        static string GetResultExample()
        {
            //This is ok, but if an error is thrown, it will be encapsulated in an AggregateException   
            return GetDataThrowsExceptionAsync().Result;
        }

        static string GetAwaiterGetResultExample()
        {
            //This is better, if an error is thrown, it will be contained in a regular Exception
            return GetDataThrowsExceptionAsync().GetAwaiter().GetResult();
        }

        #endregion

        #region Async library methods should consider using Task.ConfigureAwait(false) to boost performance

        static async Task<string> ConfigureAwaitTrueExampleAsync()
        {
            return await GetDataAsync();
        }

        static async Task<string> ConfigureAwaitFalseExampleAsync()
        {
            //It is good practice to always use ConfigureAwait(false) in library code.
            return await GetDataAsync().ConfigureAwait(false);
        }

        #endregion

        #region Cancellation of async operation

        static async Task MockMethodAsync(CancellationToken ct)
        {
            for (int i = 0; i < 100; i++)
            {
                //Simulate an async call that takes some time to complete
                await Task.Delay(1000);

                Console.WriteLine("Doing some async operation...");

                //Check if cancellation has been requested
                if (ct != null)
                {
                    ct.ThrowIfCancellationRequested();
                }
            }
        }

        #endregion
    }
}