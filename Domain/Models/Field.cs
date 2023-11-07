namespace Domain.Models;

public class Field
{
    public int Id { get; set;}
    public AuthenticationUser Owner { get; private set; }
    public string Name { get; private set; }
    
    
}