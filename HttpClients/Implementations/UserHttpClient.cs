using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient client;

    public UserHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<User> Create(UserCreationDTO dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users/CreateUser", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<UserLoginDTO> Login(UserCreationDTO dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users/login", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        UserLoginDTO user = JsonSerializer.Deserialize<UserLoginDTO>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<AuthenticationUser> GetAsync(string? username)
    {
        string query = "";
        if (!string.IsNullOrEmpty(username))
        {
            query += $"?username={username}";
        }
        
        HttpResponseMessage response = await client.GetAsync("Users/ViewProfile"+query);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        AuthenticationUser user = JsonSerializer.Deserialize<AuthenticationUser>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }
}