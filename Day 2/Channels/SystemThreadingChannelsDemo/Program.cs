using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SystemThreadingChannelsDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ExecuteUnboundedChannelExampleAsync();
            //await ExecuteBoundedChannelExampleAsync(1);
            //await ExecuteBoundedChannelWithVariousOperationsExampleAsync(10);
        }

        static async Task ExecuteUnboundedChannelExampleAsync()
        {
            // Create an unbounded channel (no limit to how many things I can put in the channel)
            var unboundedChannel = Channel.CreateUnbounded<int>();

            // Spawn a task in order to create a producer of data
            _ = Task.Run(async delegate
            {
                for (int i = 0; ; i++)
                {
                    await Task.Delay(1000);

                    unboundedChannel.Writer.TryWrite(i);
                    // await unboundedChannel.Writer.WriteAsync(i);
                }
            });

            // Consumer of data
            while (true)
            {
                Console.WriteLine(await unboundedChannel.Reader.ReadAsync());
            }
        }

        static async Task ExecuteBoundedChannelExampleAsync(int channelCapacity)
        {
            // Create a bounded channel (limiting capacity of how many things I can put in the channel)
            var boundedChannel = Channel.CreateBounded<int>(channelCapacity);

            // Spawn a task in order to create a producer of data
            _ = Task.Run(async delegate
            {
                for (int i = 0; ; i++)
                {
                    await Task.Delay(1000);

                    // Now there will be back pressure, so until the reader below picks off the next item this
                    // line will end up waiting for space to become available inside the channel
                    // The definition of "back pressure", is just the idea that if you have a producer that's
                    // running full speed ahead and generating a lot of stuff and the consumer(s) are not 
                    // consuming it as very quickly, it's gonna build up a whole lot of stuff and you
                    // will want to slow the producer down. 
                    await boundedChannel.Writer.WriteAsync(i);
                }
            });

            // Consumer of data
            while (true)
            {
                await Task.Delay(2000);

                Console.WriteLine(await boundedChannel.Reader.ReadAsync());
            }
        }

        static async Task ExecuteBoundedChannelWithVariousOperationsExampleAsync(int channelCapacity)
        {
            // Create a bounded channel 
            var boundedChannelOptions = new BoundedChannelOptions(channelCapacity)
            {
                SingleReader = true, // by informing the channel API that you will have a single reader, you allow for various optimizations to be given under the hood
                SingleWriter = true, // by informing the channel API that you will have a single writer, you allow for various optimizations to be given under the hood
                FullMode = BoundedChannelFullMode.Wait // this is the default
            };

            var boundedChannel = Channel.CreateBounded<int>(boundedChannelOptions);

            // Spawn a task in order to create a producer of data
            _ = Task.Run(async delegate
            {
                for (int i = 0; i < channelCapacity; i++)
                {
                    await Task.Delay(100);

                    await boundedChannel.Writer.WriteAsync(i);
                }

                // Writer is done and is going to say to the channel
                // no more data is going to be added to you. If any consumer(s)
                // is waiting for more data let them know that it is never going to arrive.
                boundedChannel.Writer.Complete();
            });

            // Consumer of data - Read everything till the end
            await foreach (var item in boundedChannel.Reader.ReadAllAsync())
            {
                Console.WriteLine(item);
            }

            // We can also write the same thing with the following
            //while (await boundedChannel.Reader.WaitToReadAsync())
            //{
            //    Console.WriteLine(await boundedChannel.Reader.ReadAsync());
            //}

            Console.WriteLine("Done reading data...");
        }
    }
}
