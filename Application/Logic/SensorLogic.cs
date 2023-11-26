using System.Collections;
using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class SensorLogic : ISensorLogic
{
    private readonly ISensorDao sensorDao;

    public SensorLogic(ISensorDao sensorDao)
    {
        this.sensorDao = sensorDao;
    }

    public Task<IEnumerable<SensorLookupDto>> GetAsync(int fieldId)
    {
        return sensorDao.GetSensorBySensorId(fieldId);
    }

    public Task<Sensor> CreateSensorAsync(SensorCreationDto dto)
    {
        
    }
}