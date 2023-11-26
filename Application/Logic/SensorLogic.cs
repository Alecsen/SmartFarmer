using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class SensorLogic : ISensorLogic
{
    private readonly ISensorDao sensorDao;
    private readonly IFieldDao fieldDao;
    public SensorLogic(ISensorDao sensorDao)
    {
        this.sensorDao = sensorDao;
    }

    public Task<IEnumerable<SensorLookupDto>> GetAsync(int fieldId)
    {
        return sensorDao.GetSensorBySensorId(fieldId);
    }

    public async Task<Sensor> CreateSensorAsync(SensorCreationDto dto)
    {
        var field = await fieldDao.GetFieldById(dto.FieldId);
        Random random = new Random();
        Sensor sensor = new Sensor
        {
            FieldId = field.Id,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            MoistureLevel = random.Next(0, 100)
        };

        return await sensorDao.CreateSensorAsync(sensor);
    }
}