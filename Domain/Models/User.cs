using System;
using Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public DateTime Birthday { get; set; } 
    public string Sex { get; set; }
    public string Phone { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }

    public ICollection<Field> Fields { get; set; }
}