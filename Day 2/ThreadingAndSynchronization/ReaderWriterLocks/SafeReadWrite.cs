using System;
using System.Threading;

namespace ReaderWriterLocks
{
    public class SafeReadWriter
    {
        private static ReaderWriterLockSlim rwLockSlim = new ReaderWriterLockSlim();
        private static int resource = 0; // Shared resource

        // Gets reader lock (without any timeout limit)
        public static void ReadSharedResource()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} tries to enter read lock in order to read shared resource.");

            rwLockSlim.EnterReadLock();

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} entered read lock in order to read shared resource.");

            try
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} read current resource value: {resource}");
            }
            finally
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} about to exit read lock.");

                rwLockSlim.ExitReadLock();
            }
        }

        // Tries to get writer lock with a timeout
        public static void WriteToSharedResource(int timeOutMilliSec, int value)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} tries for 1 sec to enter write lock in order to write to shared resource.");

            if (rwLockSlim.TryEnterWriteLock(timeOutMilliSec))
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} entered write lock in order to write to shared resource.");

                try
                {
                    resource = value;

                    Thread.Sleep(500);

                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} wrote at current resource value: {resource}");
                }
                finally
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} about to exit writer lock.");

                    rwLockSlim.ExitWriteLock();
                }
            }
            else
            {
                // Timeout elapsed, could not get lock
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} could not get writer lock because of the 1 sec time out.");
            }
        }

        // ReaderWriterLockSlim implements IDisposable
        ~SafeReadWriter()
        {
            // Do not forget to dispose
            if (rwLockSlim != null)
            {
                rwLockSlim.Dispose();
            }
        }
    }
}
