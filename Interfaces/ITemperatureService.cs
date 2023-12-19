namespace Temperature_Web.Interfaces
{
    public interface ITemperatureService
    {
        double? GetTemperature(string city);
    }
}
