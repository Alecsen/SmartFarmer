namespace Domain.DTOs;

public class SensorLookupDto
{
    public int Id { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public int MoistureLevel { get; set; }
}