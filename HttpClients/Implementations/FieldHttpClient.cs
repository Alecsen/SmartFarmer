using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace HttpClients.Implementations;

public class FieldHttpClient : IFieldService
{
    private readonly HttpClient client;
    private readonly AuthenticationStateProvider authenticationStateProvider;

    public FieldHttpClient(HttpClient client, AuthenticationStateProvider authenticationStateProvider)
    {
        this.client = client;
        this.authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<IEnumerable<FieldLookupDto>> GetFieldsByUserId(int userId)
    {
        HttpResponseMessage response = await client.GetAsync($"Field/{userId}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<FieldLookupDto>? fields =
            JsonSerializer.Deserialize<IEnumerable<FieldLookupDto>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return fields;
    }
}