using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWebApiDemo
{
    public class ForecastService : IForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<WeatherForecast[]> GetTodaysForecastAsync()
        {
            var rng = new Random();

            Console.WriteLine($"AspNet Core Thread Id: {Thread.CurrentThread.ManagedThreadId} in {nameof(GetTodaysForecastAsync)} before delay.");

            await Task.Delay(5000);

            Console.WriteLine($"AspNet Core Thread Id: {Thread.CurrentThread.ManagedThreadId} in {nameof(GetTodaysForecastAsync)} after delay.");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
