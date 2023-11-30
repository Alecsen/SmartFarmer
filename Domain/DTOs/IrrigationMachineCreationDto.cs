namespace Domain.DTOs;

public class IrrigationMachineCreationDto
{
    public int Id { get; set; }
    
    public int FieldId { get; set; }
    
    public double WaterAmount { get; set; }
    
    public bool IsRunning { get; set; }
}