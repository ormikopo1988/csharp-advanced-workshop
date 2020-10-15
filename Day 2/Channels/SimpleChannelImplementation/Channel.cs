using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleChannelImplementation
{
    public sealed class Channel<T>
    {
        // This will serve as our storage mechanism.
        // We use a ConcurrentQueue<T> to store the data, 
        // freeing us from needing to do our own locking to protect the buffering data structure, 
        // as ConcurrentQueue<T> is already thread-safe for any number of 
        // producers and any number of consumers to access concurrently
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();

        // This will serve as a coordinator between the producers and consumers.
        // We use a SempahoreSlim to help coordinate between producers and consumers 
        // and to notify consumers that might be waiting for additional data to arrive.
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0);

        // Our Write method gives us a method we can use to produce data into the channel, 
        // Since we decided our channel is unbounded, producing data into it will always complete successfully and synchronously, 
        // just as does calling Add on a List<T>, hence we’ve made it non-asynchronous and void-returning.
        public void Write(T value)
        {
            // Our Write method is simple. It just needs to store the data into the queue and increment the SemaphoreSlim‘s count by “release”ing it:
            _queue.Enqueue(value); // store the data

            _semaphore.Release(); // notify any consumers that more data is available
        }

        // Our ReadAsync method gives us a method to consume from it.
        // We will make our method for reading asynchronous because the data we want to consume may not yet be available.
        // Thus we’ll need to wait for it to arrive if nothing is available to consume at the time we try.
        // Since we expect to be reading frequently, and for us to often be reading when data is already available to be consumed, 
        // our ReadAsync method returns a ValueTask<T> rather than a Task<T>, 
        // so that we can make it allocation-free when it completes synchronously
        public async ValueTask<T> ReadAsync(CancellationToken cancellationToken = default)
        {
            // And our ReadAsync method is almost just as simple. 
            // It needs to wait for data to be available and then take it out.
            await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(false); // wait
            
            bool gotOne = _queue.TryDequeue(out T item); // retrieve the data

            // Note that because no other code could be manipulating the semaphore or the queue, 
            // we know that once we’ve successfully waited on the semaphore, the queue will have data to give us, 
            // which is why we can just assert that the TryDequeue method successfully returned one. 
            // If those assumptions ever changed, this implementation would need to become more complicated.
            Debug.Assert(gotOne);
            
            return item;
        }
    }
}