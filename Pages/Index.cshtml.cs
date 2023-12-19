using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Temperature_Web.Interfaces;

namespace Temperature_Web.Pages
{
    // ���������� �������� City � Temperature � ����� IndexModel � ����� ���������� ������� OnGet � OnPost
    public class IndexModel : PageModel
    {
        // ���������� ������ ITemperatureService 
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
            // ��������� ������ �� ����� appsettings.json ������ ��� ��� �� ������ ���� � ����
            string GoogleApiKey = _configuration["ApiKeys:GoogleTranslateApiKey"];
            string OpenWeatherApiKey = _configuration["ApiKeys:OpenWeatherMapApiKey"];
            Temperature = _temperatureService.GetTemperature(City, GoogleApiKey,OpenWeatherApiKey);
        }
    }
}
