namespace Domain.DTOs;

public class UserCreationDTO
{
    public string UserName { get; set; }
    public string PassWord { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; } 
    public string Sex { get; set; }
    public string Phone { get; set; }
}