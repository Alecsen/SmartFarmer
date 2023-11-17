using Application.DAOInterface;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using Moq;

namespace Application.Test;


public class FieldLogicTests
{
    private readonly Mock<IFieldDao> mockFieldDao;
    private readonly Mock<IUserDao> mockUserDao;
    private readonly FieldLogic fieldLogic;

    public FieldLogicTests()
    {
        mockFieldDao = new Mock<IFieldDao>();
        mockUserDao = new Mock<IUserDao>();
        fieldLogic = new FieldLogic(mockFieldDao.Object, mockUserDao.Object);
    }

    [Fact]
    public async Task CreateAsync_CreatesField_WhenDtoIsValid()
    {
        var fieldCreationDto = new FieldCreationDto(ownerId: 1, fieldName: "TestField");
        var expectedField = new Field { Name = fieldCreationDto.FieldName, OwnerId = fieldCreationDto.OwnerId };

        mockUserDao.Setup(dao => dao.GetByUserIdAsync(fieldCreationDto.OwnerId)).ReturnsAsync(new AuthenticationUser());
        mockFieldDao.Setup(dao => dao.CreateAsync(It.IsAny<Field>())).ReturnsAsync(expectedField);
    }
    
}