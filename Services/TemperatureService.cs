using Google.Cloud.Translation.V2;
using Newtonsoft.Json.Linq;
using System.Net;
using Temperature_Web.Interfaces;

namespace Temperature_Web.Services
{
    // реализация интерфейса ITemperatureService 
    public class TemperatureService : ITemperatureService
    {
        // ключи для доступа к API Google Translate и OpenWeatherMap 
        // метод для перевода названия города на английский язык с помощью Google Translate API
        private string TranslateCityToEnglish(string russianCity,string GoogleTranslateApiKey)
        {
            // try-catch блок для обработки исключений если ключи не верны или закончились лимиты, а также если запрос пустой
            try{
                // создание клиента для работы с Google Translate API 
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
        public double? GetTemperature(string russianCity,string GoogleTranslateApiKey,string OpenWeatherMapApiKey)
        {
            string englishCity = TranslateCityToEnglish(russianCity,GoogleTranslateApiKey);
            // try-catch блок
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
            // обработка исключений (происходит когда название города не найдено) или закончились лимиты API запросов
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
