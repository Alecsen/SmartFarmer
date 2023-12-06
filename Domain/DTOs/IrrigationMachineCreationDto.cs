namespace Domain.DTOs;

public class IrrigationMachineCreationDto
{
    public int OwnerId { get; set; }
    public double WaterAmount { get; set; }
    
    public bool IsRunning { get; set; }
    
}