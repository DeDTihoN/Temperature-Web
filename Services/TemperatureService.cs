using Google.Cloud.Translation.V2;
using Newtonsoft.Json.Linq;
using System.Net;
using Temperature_Web.Interfaces;

namespace Temperature_Web.Services
{
    public class TemperatureService : ITemperatureService
    {
        // ключи для доступа к API Google Translate и OpenWeatherMap 
        private const string GoogleTranslateApiKey = "AIzaSyAqbcpU-ZU0K_krumb6wlijpKzud3Jpej4";
        private const string OpenWeatherMapApiKey = "76b6a91eaa1f27a40f0bff71376e1b55";
        private string TranslateCityToEnglish(string russianCity)
        {
            try{
                using (TranslationClient client = TranslationClient.CreateFromApiKey(GoogleTranslateApiKey))
                {

                    var response = client.TranslateText(russianCity, "en", "ru");
                    return response.TranslatedText;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public double? GetTemperature(string russianCity)
        {
            string englishCity = TranslateCityToEnglish(russianCity);
            try
            {
                string apiKey = OpenWeatherMapApiKey;
                string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={englishCity}&units=metric&appid={apiKey}";


                // получение данных API запроса с помощью библиотеки WebClient 
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(apiUrl);
                    // парсинг данных с помощью библиотеки Newtonsoft.Json 
                    JObject data = JObject.Parse(json);
                    if (data["main"] != null && data["main"]["temp"] != null)
                    {
                        double temperature = Convert.ToDouble(data["main"]["temp"]);
                        return temperature;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
