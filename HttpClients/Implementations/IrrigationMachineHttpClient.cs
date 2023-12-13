using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.AuthServices;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class IrrigationMachineHttpClient : IIrrigationMachineService
{
    private readonly HttpClient _client;

    public IrrigationMachineHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<IrrigationMachine>> GetByOwnerId(int ownerId)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        HttpResponseMessage response = await _client.GetAsync($"IrrigationMachine/getByOwnerId/{ownerId}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<IrrigationMachine>? machines =
            JsonSerializer.Deserialize<IEnumerable<IrrigationMachine>>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return machines;
    }

    public async Task UpdateAsync(int id, int ownerId, IrrigationMachineUpdateDto dto)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.Jwt);
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await _client.PatchAsync($"IrrigationMachine/{id}/{ownerId}", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}