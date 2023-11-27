namespace Domain.Models;

public class WeatherStation
{
    public int Id { get; set; }
    public string Vindretning { get; set; }
    public int Vindhastighed { get; set; }
    public double Nedboer { get; set; }
    public int Fordampningsniveau { get; set; }
}