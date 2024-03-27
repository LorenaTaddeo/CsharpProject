using System.Text.Json;
using WeatherForecast.Models;

namespace WeatherApp.Services
{
    public class AccuWeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "1qHtKoi2jxfm2nEHPBjHGglrrcepxPUb";

        public AccuWeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherFor> GetWeatherFor(string location)
        {
            string apiUrl = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={ApiKey}&q={location}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                LocationResponse[] locations = JsonSerializer.Deserialize<LocationResponse[]>(jsonResponse);

                if (locations != null && locations.Length > 0)
                {
                    string locationKey = locations[0].Key;

                    string forecastUrl = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/{locationKey}?apikey={ApiKey}";

                    HttpResponseMessage forecastResponse = await _httpClient.GetAsync(forecastUrl);

                    if (forecastResponse.IsSuccessStatusCode)
                    {
                        string forecastJson = await forecastResponse.Content.ReadAsStringAsync();

                        DailyForecastResponse forecast = JsonSerializer.Deserialize<DailyForecastResponse>(forecastJson);

                        if (forecast != null && forecast.DailyForecasts != null && forecast.DailyForecasts.Count > 0)
                        {
                            WeatherFor weatherFor = new WeatherFor
                            {
                                LocationName = location,
                                DateTime = forecast.DailyForecasts[0].Date,
                                MinTemperature = forecast.DailyForecasts[0].Temperature.Minimum.Value,
                                MaxTemperature = forecast.DailyForecasts[0].Temperature.Maximum.Value,
                                IconPhrase = forecast.DailyForecasts[0].Day.IconPhrase
                            };
                            return weatherFor;
                        }
                    }
                }
            }
            return null;
        }
    }
}

