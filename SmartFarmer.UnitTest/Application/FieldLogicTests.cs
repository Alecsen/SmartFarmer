﻿using Application.DAOInterface;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using FluentAssertions;
using Moq;

namespace SmartFarmer.UnitTest.Application;


public class FieldLogicTests
{
    private readonly Mock<IFieldDao> mockFieldDao;
    private readonly Mock<IUserDao> mockUserDao;
    private readonly Mock<IWeatherStationDao> mockWeatherStationDao;
    private readonly FieldLogic sut;

    public FieldLogicTests()
    {
        mockFieldDao = new Mock<IFieldDao>();
        mockUserDao = new Mock<IUserDao>();
        mockWeatherStationDao = new Mock<IWeatherStationDao>();
        sut = new FieldLogic(mockFieldDao.Object, mockUserDao.Object,mockWeatherStationDao.Object);
    }

    [Fact]
    public async Task CreateAsync_CreatesField_WhenDtoIsValid()
    {

        var fieldCreationDto = new FieldCreationDto
        {
            OwnerId = 1,
            FieldName = "testField",
            LocationData = "placeholder LocationData"
        };
        
        var expectedField = new Field { Name = fieldCreationDto.FieldName, OwnerId = fieldCreationDto.OwnerId };

        mockUserDao.Setup(dao => dao.GetByUserIdAsync(fieldCreationDto.OwnerId)).ReturnsAsync(new User());
        mockFieldDao.Setup(dao => dao.CreateAsync(It.IsAny<Field>())).ReturnsAsync(expectedField);
        
        //act

        var result = await sut.CreateAsync(fieldCreationDto);

        result.Should().BeEquivalentTo(expectedField);
    }


   
}