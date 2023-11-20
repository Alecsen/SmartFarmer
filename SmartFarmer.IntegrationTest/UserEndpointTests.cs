﻿using System.Net;
using System.Net.Http.Json;
using System.Text;
using Domain.DTOs;
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
            Age = 10,
            Domain = "somethinj",
            Name = "adsasd",
            Role = "afasd",
            SecurityLevel = 10,
            Email = "alexander@gmail.comn"
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
            Age = 10,
            Domain = "somethinj",
            Name = "adsasd",
            Role = "afasd",
            SecurityLevel = 10,
        };
        var url = "/users/CreateUser";

        HttpResponseMessage response = await _client.PostAsJsonAsync("/users/CreateUser", dto);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}