using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NetCoreWebApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IForecastService _forecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastService forecastService)
        {
            _logger = logger;
            _forecastService = forecastService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            Console.WriteLine($"AspNet Core Thread Id: {Thread.CurrentThread.ManagedThreadId} starts execution in {nameof(Get)}.");

            var forecastServiceTask = _forecastService.GetTodaysForecastAsync();
            
            for(int i=0; i<20; i++)
            {
                Console.WriteLine($"AspNet Core Thread Id: {Thread.CurrentThread.ManagedThreadId} does synchronous work in {nameof(Get)}.");
            }

            var result = await forecastServiceTask;

            Console.WriteLine($"AspNet Core Thread Id: {Thread.CurrentThread.ManagedThreadId} about to return result from {nameof(Get)}.");

            return result;
        }
    }
}
