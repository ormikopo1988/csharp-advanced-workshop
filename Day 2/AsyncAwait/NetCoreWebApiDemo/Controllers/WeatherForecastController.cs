using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using NetCoreWebApiDemo.Interfaces;
using System;

namespace NetCoreWebApiDemo.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherClient _weatherClient;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeatherClient weatherClient)
        {
            _logger = logger;
            _weatherClient = weatherClient;
        }

        [HttpGet("weather/{city}")]
        public async Task<IActionResult> Forecast(string city, CancellationToken ct)
        {
            _logger.LogInformation($"AspNet Core Thread Id: {Environment.CurrentManagedThreadId} starts execution in {nameof(Forecast)}.");

            var weatherTask = _weatherClient.GetCityWeatherAsync(city, ct);

            for (var i = 0; i < 5; i++)
            {
                _logger.LogInformation($"AspNet Core Thread Id: {Environment.CurrentManagedThreadId} does synchronous work in {nameof(Forecast)}.");
            }

            var weather = await weatherTask;

            _logger.LogInformation($"AspNet Core Thread Id: {Environment.CurrentManagedThreadId} about to return result from {nameof(Forecast)}.");

            return weather is not null ? Ok(weather) : NotFound();
        }
    }
}