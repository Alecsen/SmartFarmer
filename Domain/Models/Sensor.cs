namespace Domain.Models;

public class Sensor
{
    public int Id { get; set; }
    public int FieldId { get; set; }
    public Field Field { get; private set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public int MoistureLevel { get; set; }
    public int soiltype { get; set; }
}