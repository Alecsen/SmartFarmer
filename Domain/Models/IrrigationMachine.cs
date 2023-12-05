namespace Domain.Models;

public class IrrigationMachine
{
    public int Id { get; set; }
    
    public int OwnerId { get; set; }
    public int FieldId { get; set; }
    
    public double WaterAmount { get; set; }
    
    public bool IsRunning { get; set; }
    
}