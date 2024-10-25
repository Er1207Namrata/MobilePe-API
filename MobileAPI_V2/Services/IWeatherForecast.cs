using MobileAPI_V2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Services
{
     public interface IWeatherForecast
    {
        Task<ServicesResponse<IEnumerable<WeatherForecast>>> Getweather();
    }
}
