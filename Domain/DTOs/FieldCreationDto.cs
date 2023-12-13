using Microsoft.AspNetCore.Mvc;

namespace Domain.DTOs;

public class FieldCreationDto
{
    public int OwnerId { get; set; }
    public string FieldName { get; set; }
    public string? LocationData { get; set; }
    public string CropType { get; set; }
    public int SoilType { get; set; }
}