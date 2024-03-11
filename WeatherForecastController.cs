using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get([FromQuery] WeatherRequest request)
        {
            var rng = new Random();
			var forecasts = new List<WeatherForecast>();
			for (int i = 0; i < request.numberOfDays; i++)
			{
				var temperatureC = rng.Next(request.minValue, request.maxValue);
				var temperatureF = 32 + (int)(temperatureC / 0.5556);
				forecasts.Add(new WeatherForecast
                {
					Date = DateTime.Now.AddDays(i),
					TemperatureC = temperatureC,
					TemperatureF = temperatureF,
					Summary = Summaries[rng.Next(Summaries.Length)]
				});
			}
			return forecasts;
        }

        [HttpPost]
        public string Post(WeatherForecast forecast)
        {

            return "Ok, i will post tomorrow.";
        }
    }
}