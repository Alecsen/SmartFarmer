namespace Domain.DTOs;

public class ProfileUpdateDto
{
    public string Username { get; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    public ProfileUpdateDto(string username)
    {
        Username = username;
    }
}