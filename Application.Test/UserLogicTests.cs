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

    [Fact]
    public async Task CreateAsync_CreatesUser_WhenAllConditionsAreMet()
    {
        // Arange

        var userCreationDto = new UserCreationDTO
        {
            UserName = "newUser"
        };

        userDaoMock.Setup(dao => dao.GetByUsernameAsync(userCreationDto.UserName)).ReturnsAsync(() =>null);
        userDaoMock.Setup(dao => dao.CreateAsync(It.IsAny<AuthenticationUser>())).ReturnsAsync(new AuthenticationUser());
        // Act

        var result = await sut.CreateAsync(userCreationDto);
        
        // Assert
        Assert.NotNull(result);
        userDaoMock.Verify(dao => dao.CreateAsync(It.IsAny<AuthenticationUser>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_DoesNotCreateUser_WhenUserAlreadyExists()
    {
        //Arange

        var usercreationdto = new UserCreationDTO
        {
            UserName = "newUser"
        };


        userDaoMock.Setup(dao => dao.GetByUsernameAsync(usercreationdto.UserName))
            .ReturnsAsync(new AuthenticationUser());
        
        //Assert
        await Assert.ThrowsAsync<Exception>(() => sut.CreateAsync(usercreationdto));
        userDaoMock.Verify(dao => dao.CreateAsync(It.IsAny<AuthenticationUser>()), Times.Never);
    }

    [Theory]
    [InlineData("ab")]
    [InlineData("thisUserNameIsWayTooLongForTheSystem")]
    public async Task CreateAsync_ThrowsException_ForInvalidUsernames(string invalidUsername)
    {
        var userCreationDto = new UserCreationDTO
        {
            UserName = invalidUsername,
        };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => sut.CreateAsync(userCreationDto));
    }

    [Fact]
    public async Task ValidateLogin_ReturnsAUser_WhenAllConditionAreMet()
    {
        //Arrange 

        var authUserLoginDto = new AuthUserLoginDto
        {
            Username = "alecsen",
            Password = "123"
        };

        var authenticationUser = new AuthenticationUser
        {
            Username = "alecsen",
            Password = "123"
        };

        userDaoMock.Setup(dao => dao.GetByUsernameAsync(authUserLoginDto.Username)).ReturnsAsync(authenticationUser);
        
        // act

        var result = await sut.ValidateLogin(authUserLoginDto);
        
        //assert
        result.Should().BeEquivalentTo(authenticationUser);
    }

    [Fact]
    public async Task validtateLogin_ThrowsAnException_WhentheUserDoesntExist()
    {
        //Arrange
        var authUserLoginDto = new AuthUserLoginDto
        {
            Username = "alecsen",
            Password = "123"
        };
        userDaoMock.Setup(dao => dao.GetByUsernameAsync(authUserLoginDto.Username)).ReturnsAsync(() => null);
        
        //act

        Func<Task> act = async () =>  await sut.ValidateLogin(authUserLoginDto);
        await act.Should().ThrowAsync<Exception>();
        
    }

    [Fact]
    public async Task ValidateLogin_ThrowsAnException_WhenThePassWordIsWrong()
    {
        var authUserLoginDto = new AuthUserLoginDto
        {
            Username = "alecsen",
            Password = "WrongPassWord"
        };
        var authenticationUser = new AuthenticationUser
        {
            Username = "alecsen",
            Password = "RightPassword"
        };


        userDaoMock.Setup(dao => dao.GetByUsernameAsync(authUserLoginDto.Username)).ReturnsAsync(authenticationUser);

        Func<Task> act = async () => await sut.ValidateLogin(authUserLoginDto);
        await act.Should().ThrowAsync<Exception>(); // checks that it throws our pass word exception
    }
}