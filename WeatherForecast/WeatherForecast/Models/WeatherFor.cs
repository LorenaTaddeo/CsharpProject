using System;
namespace WeatherForecast.Models
{
	public class WeatherFor
	{
        public int Id { get; set; }
        public string LocationName { get; set; }
        public DateTime DateTime { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public string IconPhrase { get; set; }
	}
}