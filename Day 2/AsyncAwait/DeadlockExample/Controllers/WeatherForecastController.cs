using System.Collections.Generic;
using System.Web.Http;
using MockLibrary;

namespace NetCoreWebApiDemo.Controllers
{
    public class WeatherForecastController : ApiController
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var forecastService = new ForecastService();

            var forecastServiceTask = forecastService.GetTodaysForecastAsync();
            
            var result = forecastServiceTask.Result;

            return result;
        }
    }
}
