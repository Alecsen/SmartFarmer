using System.Net;
using System.Net.Http.Json;
using System.Text;
using Domain.DTOs;
using Domain.Models;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace SmartFarmer.IntegrationTest;

public class UserEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly ITestOutputHelper testOutputHelper;
    private readonly HttpClient _client;

    public UserEndpointTests(CustomWebApplicationFactory factory, ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
        _client = factory.CreateClient();
        factory.SeedDatabase(); // Seed databasen én gang for alle tests i denne klasse
    }

    [Fact]
    public async Task Create_User_ReturnsCreatedResponsWithUserData()
    {
        var dto = new UserCreationDTO()
        {
            UserName = "wuwuwuwuwu",
            PassWord = "passWord",
            Name = "adsasd",
            Role = "afasd",
            Email = "alexander@gmail.comn",
            Birthday = DateTime.Now,
            Address = "adasdas",
            Phone = "4553386087",
            Sex = "Yes"
        };
        var url = "/users/CreateUser";

        HttpResponseMessage response = await _client.PostAsJsonAsync("/users/CreateUser", dto);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task CreateA_User_ReturnsBadRequestWhenDataIsMising()
    {
        var dto = new UserCreationDTO()
        {
            UserName = "wuwuwuwuwu",
            PassWord = "passWord",
            Name = "adsasd",
            Role = "afasd",
        };
        var url = "/users/CreateUser";

        HttpResponseMessage response = await _client.PostAsJsonAsync("/users/CreateUser", dto);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Fact]
    public async Task Get_UserProfile_ReturnsUserData()
    {
        // Arrange
        var username = "Alecsen"; // Erstat med et gyldigt brugernavn
        var url = $"Users/ViewProfile?username={username}";

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var responseContent = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<User>(responseContent);
        user.Should().NotBeNull();
        user.Username.Should().Be(username);
    }
    
    [Fact]
    public async Task Update_UserProfile_ReturnsSuccess()
    {
        // Arrange
        var dto = new ProfileUpdateDto("alecsen");
        
        var json = JsonConvert.SerializeObject(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var url = "Users/EditUser";

        // Act
        var response = await _client.PatchAsync(url, content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
    }
}