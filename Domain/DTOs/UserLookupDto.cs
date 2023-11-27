namespace Domain.DTOs;

public class UserLookupDto
{
    public string Username { get; set; }

    public UserLookupDto(string username)
    {
        Username = username;
    }
}