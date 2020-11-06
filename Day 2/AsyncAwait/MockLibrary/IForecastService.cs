using System.Threading.Tasks;

namespace MockLibrary
{
    public interface IForecastService
    {
        Task<WeatherForecast[]> GetTodaysForecastAsync();
    }
}