namespace Temperature_Web.Interfaces
{
    // интерфейс сервиса для получения температуры 
    public interface ITemperatureService
    {
        double? GetTemperature(string city,string GoogleApiKey,string OpenWeatherKey);
    }
}
