using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterface;

public interface ISensorDao
{
    Task<IEnumerable<SensorLookupDto>> GetSensorBySensorId(int fieldId);
    Task<Sensor> CreateSensorAsync (Sensor sensor);
}