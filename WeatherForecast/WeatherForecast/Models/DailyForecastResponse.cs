

namespace WeatherForecast.Models
{
	public class DailyForecastResponse
	{
        public LocationResponse Location { get; set; }
        public List<DailyForecast> DailyForecasts { get; set; }
	}
}