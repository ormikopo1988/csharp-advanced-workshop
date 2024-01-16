using NetCoreWebApiDemo.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreWebApiDemo.Interfaces
{
    public interface IWeatherClient
    {
        Task<WeatherResponse?> GetCityWeatherAsync(string city, CancellationToken ct);
    }
}
