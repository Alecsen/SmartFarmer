
using System.Net;
using Domain.DTOs;
using Newtonsoft.Json;

namespace SmartFarmer.IntegrationTest;

public class FieldEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public FieldEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_FieldsByOwner_ReturnsFields()
    {
        // Arrange
        var ownerId = 1; // Erstat med en gyldig ejer ID
        var url = $"/Field/{ownerId}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Dette vil fejle, hvis statuskoden ikke er 200-serien
        var responseString = await response.Content.ReadAsStringAsync();
        var fields = JsonConvert.DeserializeObject<IEnumerable<FieldLookupDto>>(responseString);

        Assert.NotNull(fields); // Sikrer, at vi faktisk fik nogle data tilbage
        Assert.NotEmpty(fields); // Sikrer, at ejeren har mindst ét felt
    }

    [Fact]
    public async Task Get_FieldByOwner_DoesNotReturnFieldWhenWrongOwnerId()
    {
        // Arrange
        var ownerId = 1000; // Erstat med en ugyldig ejer ID
        var url = $"/Field/{ownerId}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
    }
}
