using System.Collections.Generic;
using System.Threading;
using System;

var threads = new List<Thread>
{
    new Thread(SafeReadWriter.ReadSharedResource),
    new Thread(SafeReadWriter.ReadSharedResource),
    new Thread(SafeReadWriter.ReadSharedResource),
    new Thread(() =>
    {
        SafeReadWriter.WriteToSharedResource(1000, 30);
    }),
    new Thread(SafeReadWriter.ReadSharedResource),
    new Thread(SafeReadWriter.ReadSharedResource),
    new Thread(SafeReadWriter.ReadSharedResource),
    new Thread(SafeReadWriter.ReadSharedResource),
    new Thread(() =>
    {
        SafeReadWriter.WriteToSharedResource(1000, 50);
    })
};

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