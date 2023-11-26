using Application.DAOInterface;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using FluentAssertions;
using Moq;

namespace SmartFarmer.UnitTest.Application;

public class SensorLogicTest
{
    private readonly Mock<IFieldDao> mockFieldDao;
    private readonly Mock<ISensorDao> mockSensorDao;
    private readonly SensorLogic sut;


    public SensorLogicTest()
    {
        mockFieldDao = new Mock<IFieldDao>();
        mockSensorDao = new Mock<ISensorDao>();
        sut = new SensorLogic(mockSensorDao.Object, mockFieldDao.Object);
    }

    [Fact]
    public async Task CreateSensorAsync_CreateSensor_WhenAllCondtionsAreMet()
    {
        var dto = new SensorCreationDto
        {
            FieldId = 1,
            Longitude = 60.050,
            Latitude = 69.5432
        };

        var expectedSensor = new Sensor
        {
            FieldId = dto.FieldId,
            Longitude = dto.Longitude,
            Latitude = dto.Latitude
        };

        mockFieldDao.Setup(dao => dao.GetFieldById(dto.FieldId)).ReturnsAsync(new Field());
        mockSensorDao.Setup(dao => dao.CreateSensorAsync(It.IsAny<Sensor>())).ReturnsAsync(expectedSensor);

        var result = await sut.CreateSensorAsync(dto);

        result.Should().Be(expectedSensor);
    }
}