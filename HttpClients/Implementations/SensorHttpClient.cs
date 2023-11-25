using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class SensorHttpClient : ISensorService
{
    private readonly HttpClient client;

    public SensorHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<IEnumerable<SensorLookupDto>> GetSensorsByFieldId(int fieldId)
    {
        HttpResponseMessage response = await client.GetAsync($"Sensor/{fieldId}");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<SensorLookupDto> sensors = JsonSerializer.Deserialize<IEnumerable<SensorLookupDto>>(result,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return sensors;
    }
}