namespace Domain.DTOs;

public class UserSearchParametersDTO
{
    public string Username { get; set; }

    public UserSearchParametersDTO(string username)
    {
        Username = username;
    }
}