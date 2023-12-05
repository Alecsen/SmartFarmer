using System.Net.Http.Json;
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
        HttpResponseMessage response = await client.GetAsync($"Field/FieldOwner/{userId}");
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

    public async Task<Field> GetFieldById(int fieldId)
    {
        HttpResponseMessage response = await client.GetAsync($"/Field/{fieldId}");
        String result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Field field = JsonSerializer.Deserialize<Field>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return field; 
    }

    public async Task<Field> CreateField(FieldCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Field", dto);
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Field field = JsonSerializer.Deserialize<Field>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return field; 
    }
}