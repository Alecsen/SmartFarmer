namespace Domain.Models;

public class WeatherStation
{
    public int Id { get; set; }
    public int FieldId { get; set; }
    public string WindDirection { get; set; }
    public int WindSpeed { get; set; }
    public double Precipitation { get; set; }
    public double Evaporation { get; set; }
}