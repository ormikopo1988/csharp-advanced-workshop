using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncMethodReturnTypes
{
    class Program
    {
        static Random rnd;

        static async Task Main(string[] args)
        {
            // Task<TResult> Return Type
            Console.WriteLine(await ShowTodaysInfoAsync());

            // Task Return Type
            await DisplayCurrentInfoAsync();

            // Generalized async return types and ValueTask<TResult>
            Console.WriteLine($"You rolled {await GetDiceRollAsync()}");

            // Async streams with IAsyncEnumerable<T>
            await foreach (var word in ReadWordsFromStreamAsync())
            {
                Console.WriteLine($"Next word read is: {word}");
            }
        }

        #region Task<TResult> Return Type

        static async Task<string> ShowTodaysInfoAsync()
        {
            string ret = $"Today is {DateTime.Today:D}\n" +
                         "Today's hours of leisure: " +
                         $"{await GetLeisureHoursAsync()}";

            return ret;
        }

        static async Task<int> GetLeisureHoursAsync()
        {
            // Task.FromResult is a placeholder for actual work that returns a string.
            var today = await Task.FromResult<string>(DateTime.Now.DayOfWeek.ToString());

            // The method then can process the result in some way.
            int leisureHours;

            if (today.First() == 'S')
            {
                leisureHours = 16;
            }
            else
            {
                leisureHours = 5;
            }

            return leisureHours;
        }

        #endregion

        #region Task Return Type

        static async Task DisplayCurrentInfoAsync()
        {
            await WaitAndApologizeAsync();
            Console.WriteLine($"Today is {DateTime.Now:D}");
            Console.WriteLine($"The current time is {DateTime.Now.TimeOfDay:t}");
            Console.WriteLine("The current temperature is 76 degrees.");
        }

        static async Task WaitAndApologizeAsync()
        {
            // Task.Delay is a placeholder for actual work.
            await Task.Delay(2000);

            // Task.Delay delays the following line by two seconds.
            Console.WriteLine("\nSorry for the delay. . . .\n");
        }

        #endregion

        #region Generalized async return types and ValueTask<TResult>

        static async ValueTask<int> GetDiceRollAsync()
        {
            Console.WriteLine("...Shaking the dices...");

            int roll1 = await RollAsync();
            int roll2 = await RollAsync();
            
            return roll1 + roll2;
        }

        static async ValueTask<int> RollAsync()
        {
            if (rnd == null)
            {
                rnd = new Random();
            }   

            var delayTask = Task.Delay(50);
            
            int diceRoll = rnd.Next(1, 7);

            // Audience Question: Why is this a good candidate to use ValueTask? Will execution be synchronous or asynchronous here in this particular await?
            await delayTask;

            return diceRoll;
        }

        #endregion

        #region Async streams with IAsyncEnumerable<T>

        static async IAsyncEnumerable<string> ReadWordsFromStreamAsync()
        {
            string data =
                @"This is a line of text.
                Here is the second line of text.
                And there is one more for good measure.
                Wait, that was the penultimate line.";

            using (var readStream = new StringReader(data))
            {
                var line = await readStream.ReadLineAsync();

                while (line != null)
                {
                    var words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    foreach (var word in words)
                    {
                        yield return word;
                    }

                    line = await readStream.ReadLineAsync();
                }
            }
        }

        #endregion
    }
}