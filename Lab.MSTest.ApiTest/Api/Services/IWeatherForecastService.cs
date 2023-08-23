using Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IWeatherForecastService
    {
        Task<IReadOnlyList<WeatherForecast>> WeatherForecasts(string[] summaries);
    }
}
