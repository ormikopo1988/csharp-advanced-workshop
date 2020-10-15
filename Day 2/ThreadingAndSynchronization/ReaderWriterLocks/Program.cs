using System;
using System.Collections.Generic;
using System.Threading;

namespace ReaderWriterLocks
{
    class Program
    {
        static void Main(string[] args)
        {
            var threads = new List<Thread>();

            threads.Add(new Thread(SafeReadWriter.ReadSharedResource));
            threads.Add(new Thread(SafeReadWriter.ReadSharedResource));
            threads.Add(new Thread(SafeReadWriter.ReadSharedResource));
            threads.Add(new Thread(() =>
            {
                SafeReadWriter.WriteToSharedResource(1000, 30);
            }));
            threads.Add(new Thread(SafeReadWriter.ReadSharedResource));
            threads.Add(new Thread(SafeReadWriter.ReadSharedResource));
            threads.Add(new Thread(SafeReadWriter.ReadSharedResource));
            threads.Add(new Thread(SafeReadWriter.ReadSharedResource));
            threads.Add(new Thread(() =>
            {
                SafeReadWriter.WriteToSharedResource(1000, 50);
            }));

            foreach (var thread in threads)
            {
                thread.Start();
            }

            SafeReadWriter.WriteToSharedResource(1000, 100);

            foreach (var thread in threads)
            {
                thread.Join();
            }

            SafeReadWriter.ReadSharedResource();

            Console.ReadLine();
        }
    }
}
