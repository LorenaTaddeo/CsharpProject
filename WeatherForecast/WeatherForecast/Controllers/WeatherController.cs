using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;
using WeatherForecast.Models;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly AccuWeatherService _accuWeatherService;
        
        private readonly WeatherService _weatherService; 

        public WeatherForecastController(AccuWeatherService accuWeatherService, WeatherService weatherService) 
        {
            _accuWeatherService = accuWeatherService;
            _weatherService = weatherService; 
        }

        [HttpGet]
        public async Task<ActionResult<WeatherFor>> GetWeatherForecast(string locationQuery)
        {
            try
            {
                var weatherForecast = await _accuWeatherService.GetWeatherFor(locationQuery);

                var weatherFor = new WeatherFor
                    {
                        LocationName = weatherForecast.LocationName,
                        DateTime = DateTime.Now, 
                        MinTemperature = weatherForecast.MinTemperature,
                        MaxTemperature = weatherForecast.MaxTemperature,
                        IconPhrase = weatherForecast.IconPhrase
                    };
                await _weatherService.SaveWeatherData(weatherFor);

                return Ok(weatherForecast);
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore durante l'ottenimento delle previsioni del tempo: {ex.Message}");
            }
        }
    }
}