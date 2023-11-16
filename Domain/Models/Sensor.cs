namespace Domain.Models;

public class Sensor
{
    public int Id { get; set; }
    public int FieldId { get; set; }
    public Field Field { get; private set; }
    public int Longitude { get; set; }
    public int Latitude { get; set; }
    public int MoistureLevel { get; set; }
    public int soiltype { get; set; }
}