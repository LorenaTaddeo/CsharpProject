using Microsoft.Extensions.Logging;
using WeatherApp.Data;
using WeatherForecast.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly WeatherDbContext _dbContext;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(WeatherDbContext dbContext, ILogger<WeatherService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> SaveWeatherData(WeatherFor weatherFor)
        {
            try{
                weatherFor.DateTime = weatherFor.DateTime.ToUniversalTime();

                _dbContext.WeatherFor.Add(weatherFor);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Si è verificato un errore durante il salvataggio dei dati nel database.");
                return false;
            }
        }
    }
}
