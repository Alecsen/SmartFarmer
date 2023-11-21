
using System.Net;
using Domain.DTOs;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace SmartFarmer.IntegrationTest;

public class FieldEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient client;

    public FieldEndpointTests(CustomWebApplicationFactory factory)
    {
        client = factory.CreateClient();
        factory.SeedDatabase(); 
    }

    [Fact]
    public async Task Get_FieldsByOwner_ReturnsFields()
    {
        // Arrange
        var ownerId = 1; 
        var url = $"/Field/{ownerId}";

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); 
        var responseString = await response.Content.ReadAsStringAsync();
        var fields = JsonConvert.DeserializeObject<IEnumerable<FieldLookupDto>>(responseString);

        Assert.NotNull(fields); 
        Assert.NotEmpty(fields); 
    }

    [Fact]
    public async Task Get_FieldByOwner_DoesNotReturnFieldWhenWrongOwnerId()
    {
        // Arrange
        var ownerId = 1000; 
        var url = $"/Field/{ownerId}";

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
    
    [Fact]
    public async Task Get_FieldByOwner_ReturnsBadRequestForInvalidInputMinus1()
    {
        // Arrange
        var ownerId = -1;
        var url = $"/Field/{ownerId}";
        
        // Act
        var response = await client.GetAsync(url);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
 

}
