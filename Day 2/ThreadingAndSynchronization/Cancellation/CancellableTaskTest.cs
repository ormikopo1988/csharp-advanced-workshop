using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cancellation
{
    public class CancellableTaskTest
    {
        public Task CancellableTask(CancellationToken ct)
        {
            return Task.Factory.StartNew(() => CancellableWork(ct), ct);
        }

        private void CancellableWork(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Cancelled work before start");

                cancellationToken.ThrowIfCancellationRequested();
            }

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);

                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine($"Cancelled on iteration # {i + 1}");

                    // The following lien alone is enough to check and throw
                    cancellationToken.ThrowIfCancellationRequested();
                }
                
                Console.WriteLine($"Iteration # {i + 1} completed");
            }
        }
    }
}