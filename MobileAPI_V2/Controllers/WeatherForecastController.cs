using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobileAPI_V2.Model;
using MobileAPI_V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI_V2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastServices _ws;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,WeatherForecastServices ws)
        {
            _logger = logger;
            _ws = ws;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_ws.Getweather());
        }
    }
}
