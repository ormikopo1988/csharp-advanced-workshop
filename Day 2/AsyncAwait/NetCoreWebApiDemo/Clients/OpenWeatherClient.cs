using Microsoft.Extensions.Logging;
using NetCoreWebApiDemo.Interfaces;
using NetCoreWebApiDemo.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWebApiDemo.Clients
{
    public class OpenWeatherClient : IWeatherClient
    {
        public const string ClientName = "weatherapi";

        /// <summary>
        /// Please use this wisely.
        /// Also remember to NEVER include secrets inside the code itself and commit them to source control :-)
        /// Price plan: Free 
        /// Limits:
        ///   - Hourly forecast: unavailable
        ///   - Daily forecast: unavailable
        ///   - Calls per minute: 60
        ///   - 3 hour forecast: 5 days
        /// https://openweathermap.org/price#weather
        /// </summary>
        private const string OpenWeatherMapApiKey = "666bd600122fe322de08a7ebc9327c69";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OpenWeatherClient> _logger;

        public OpenWeatherClient(IHttpClientFactory httpClientFactory, ILogger<OpenWeatherClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<WeatherResponse?> GetCityWeatherAsync(string city, CancellationToken ct)
        {
            var client = _httpClientFactory.CreateClient(ClientName);

            _logger.LogInformation($"AspNet Core Thread Id: {Environment.CurrentManagedThreadId} in {nameof(GetCityWeatherAsync)} about to call OpenWeather API.");

            var response = await client.GetAsync($"weather?q={city}&appid={OpenWeatherMapApiKey}", ct);

            return await response.Content.ReadFromJsonAsync<WeatherResponse>(cancellationToken: ct);
        }
    }
}
