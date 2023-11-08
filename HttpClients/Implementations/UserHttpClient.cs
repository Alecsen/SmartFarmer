using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient client;
    private readonly AuthenticationStateProvider authenticationStateProvider;

    public UserHttpClient(HttpClient client, AuthenticationStateProvider authenticationStateProvider)
    {
        this.client = client;
        this.authenticationStateProvider = authenticationStateProvider;
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

    public async Task<int> GetCurrentUserId()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        var userIdClaim = user.FindFirst("UserId");
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }

        return -1; // Or throw an exception or return a nullable int if you prefer
    }
}