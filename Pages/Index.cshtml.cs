using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Temperature_Web.Interfaces;

namespace Temperature_Web.Pages
{
    // добавление свойства City и Temperature в класс IndexModel и также добавление методов OnGet и OnPost
    public class IndexModel : PageModel
    {
        // внедренный сервис ITemperatureService 
        private readonly ITemperatureService _temperatureService;
        private readonly IConfiguration _configuration;
        [BindProperty]
        public string City { get; set; }

        public double? Temperature { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ITemperatureService temperatureService, IConfiguration configuration)
        {
            _logger = logger;
            _temperatureService = temperatureService;
            _configuration = configuration;
        }


        public void OnPost()
        {
            // получение ключей из файла appsettings.json потому что они не должны быть в коде
            string GoogleApiKey = _configuration["ApiKeys:GoogleTranslateApiKey"];
            string OpenWeatherApiKey = _configuration["ApiKeys:OpenWeatherMapApiKey"];
            Temperature = _temperatureService.GetTemperature(City, GoogleApiKey,OpenWeatherApiKey);
        }
    }
}
