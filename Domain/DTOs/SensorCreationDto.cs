using Domain.Models;

namespace Domain.DTOs;

public class SensorCreationDto
{
    public int FieldId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}