using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting application...");

            CancellationTokenSource source = new CancellationTokenSource();

            // Assuming the wrapping class is CancellableTaskTest
            var task = new CancellableTaskTest().CancellableTask(source.Token);

            Console.WriteLine("Heavy process invoked");

            Console.WriteLine("Press C to cancel");
            Console.WriteLine("");

            char ch = Console.ReadKey().KeyChar;
            
            if (ch == 'c' || ch == 'C')
            {
                source.Cancel();

                Console.WriteLine("\nTask cancellation requested.");
            }

            try
            {
                task.Wait();
            }
            catch (AggregateException ae)
            {
                if (ae.InnerExceptions.Any(e => e is TaskCanceledException))
                {
                    Console.WriteLine("Task cancelled exception detected");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                source.Dispose();
            }

            Console.WriteLine("Process completed");

            Console.ReadKey();
        }
    }
}
