using System.Threading.Tasks;

namespace NetCoreWebApiDemo
{
    public interface IForecastService
    {
        Task<WeatherForecast[]> GetTodaysForecastAsync();
    }
}