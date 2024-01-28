using System;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncMethodReturnTypes
{
    class Program
    {
        static Random? randomNumberGenerator;

        static async Task Main(string[] args)
        {
            // Task<TResult> Return Type
            Console.WriteLine(await ShowTodaysInfoAsync());

            // Task Return Type
            await DisplayCurrentInfoAsync();

            // Generalized async return types and ValueTask<TResult>
            Console.WriteLine($"You rolled {await GetDiceRollAsync()}");
        }

        #region Task<TResult> Return Type

        static async Task<string> ShowTodaysInfoAsync()
        {
            var ret = $"Today is {DateTime.Today:D}\n" +
                         "Today's hours of leisure: " +
                         $"{await GetLeisureHoursAsync()}";

            return ret;
        }

        static async Task<int> GetLeisureHoursAsync()
        {
            // Task.FromResult is a placeholder for actual work that returns a string.
            var today = await Task.FromResult(DateTime.Now.DayOfWeek.ToString());

            // The method then can process the result in some way.
            int leisureHours;

            if (today.First() == 'S')
            {
                leisureHours = 16;
            }
            else
            {
                leisureHours = 8;
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

            var roll1 = await RollAsync();
            var roll2 = await RollAsync();

            return roll1 + roll2;
        }

        static async ValueTask<int> RollAsync()
        {
            randomNumberGenerator ??= new Random();

            var diceRoll = randomNumberGenerator.Next(1, 7);

            if (diceRoll == 6)
            {
                // Task.Delay is a placeholder for actual work
                // that needs to be done asynchronously whenever we 
                // get a lucky 6!
                await Task.Delay(5000);
            }

            return diceRoll;
        }

        #endregion
    }
}