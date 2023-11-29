namespace Domain.DTOs;

public class FieldLookupDto
{
    public int Id { get; set;}
    public string FieldName { get; set; }
    
    public string? locationData { get; set; }
    
    public double FieldCapacity { get; set; }
    public double MoistureLevel { get; set; }
}