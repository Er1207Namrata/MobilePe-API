using MobileAPI_V2.Model;
using MobileAPI_V2.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Services
{
    public class WeatherForecastServices : IWeatherForecast
    {
        Summaries Summaries = new Summaries();
        public async Task<ServicesResponse<IEnumerable<WeatherForecast>>> Getweather()
        {
            ServicesResponse<IEnumerable<WeatherForecast>> servicesresponse = new ServicesResponse<IEnumerable<WeatherForecast>>();
            try
            {
                var rng = new Random();
                servicesresponse.Data=Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries.Chilly.ToString()
                })
                .ToList();
            }
            catch(Exception ex)
            {
                servicesresponse.response = false;
                servicesresponse.message = ex.Message;
            }

            return servicesresponse;
           
        }
    }
}
