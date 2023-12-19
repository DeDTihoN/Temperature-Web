using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Temperature_Web.Interfaces;

namespace Temperature_Web.Pages
{
    // добавление свойства City и Temperature в класс IndexModel и также добавление методов OnGet и OnPost
    public class IndexModel : PageModel
    {
        private readonly ITemperatureService _temperatureService;

        [BindProperty]
        public string City { get; set; }

        public double? Temperature { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ITemperatureService temperatureService)
        {
            _logger = logger;
            _temperatureService = temperatureService;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            Temperature = _temperatureService.GetTemperature(City);
        }
    }
}
