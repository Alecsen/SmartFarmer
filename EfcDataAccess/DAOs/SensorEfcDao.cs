using Application.DAOInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess.DAOs;

public class SensorEfcDao: ISensorDao
{
    private readonly SmartFarmerAppContext context;

    public SensorEfcDao(SmartFarmerAppContext context)
    {
        this.context = context;
    }
    
    public async Task<IEnumerable<SensorLookupDto>> GetSensorBySensorId(int fieldId)
    {
        var sensors = await context.Sensors
            .Where(sensor => sensor.FieldId == fieldId )
            .ToListAsync();
        if (!sensors.Any())
        {
            throw new Exception($"Field {fieldId} does not have any sensors");
        }

        List<SensorLookupDto> result = new List<SensorLookupDto>();

        foreach (Sensor sensor in sensors)
        {
            SensorLookupDto dto = new SensorLookupDto();
            dto.Id = sensor.Id;
            dto.Latitude = sensor.Latitude;
            dto.Longitude = sensor.Longitude;
            dto.MoistureLevel = sensor.MoistureLevel;
            
            result.Add(dto);
        }

        return result;
    }
}