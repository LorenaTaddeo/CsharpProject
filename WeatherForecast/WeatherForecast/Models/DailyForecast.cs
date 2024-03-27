

namespace WeatherForecast.Models
{
	public class DailyForecast
	{
        public TemperatureData Temperature { get; set; }
        public DayForecast Day { get; set; }
        public DateTime Date { get; set; }
	}
}

