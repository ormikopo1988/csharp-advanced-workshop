using System.Collections.Generic;
using System.Threading;
using System;
using ReaderWriterLocks;

var threads = new List<Thread>
{
    new(SafeReadWriter.ReadSharedResource),
    new(SafeReadWriter.ReadSharedResource),
    new(SafeReadWriter.ReadSharedResource),
    new(() =>
    {
        SafeReadWriter.WriteToSharedResource(1000, 30);
    }),
    new(SafeReadWriter.ReadSharedResource),
    new(SafeReadWriter.ReadSharedResource),
    new(SafeReadWriter.ReadSharedResource),
    new(SafeReadWriter.ReadSharedResource),
    new(() =>
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