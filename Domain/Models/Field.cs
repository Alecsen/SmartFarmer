namespace Domain.Models;

public class Field
{
    public int Id { get; set;}
    
    public int OwnerId { get; set; }
    public AuthenticationUser Owner { get; set; }
    public string Name { get; set; }
    
}