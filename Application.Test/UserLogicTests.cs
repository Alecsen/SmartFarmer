using Application.DAOInterface;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using FluentAssertions;
using Moq;

namespace Application.Test;

public class UserLogicTests
{
    private readonly UserLogic sut;
    private readonly Mock<IUserDao> userDaoMock;

    public UserLogicTests()
    {
       userDaoMock = new Mock<IUserDao>();
       sut = new UserLogic(userDaoMock.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userName = "alecsen";
        var UserSearchParameters = new UserSearchParametersDTO(userName);
        var expectedUser = new AuthenticationUser { Username = userName };

        userDaoMock.Setup(dao => dao.GetByUsernameAsync(userName)).ReturnsAsync(expectedUser);
        
        // Act
        var user = await sut.GetAsync(UserSearchParameters);

        // Assert
        user.Should().NotBeNull();
        Assert.Equal(userName, user.Username);
    }
}