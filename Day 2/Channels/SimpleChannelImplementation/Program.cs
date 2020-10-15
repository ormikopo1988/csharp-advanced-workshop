using System;
using System.Threading.Tasks;

namespace SimpleChannelImplementation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = new Channel<int>();

            // Spawn a task in order to create a producer of data
            _ = Task.Run(async delegate
            {
                for (int i = 0; ; i++)
                {
                    await Task.Delay(1000);

                    channel.Write(i);
                }
            });

            // Consumer of data
            while(true)
            {
                Console.WriteLine(await channel.ReadAsync());
            }
        }
    }
}
