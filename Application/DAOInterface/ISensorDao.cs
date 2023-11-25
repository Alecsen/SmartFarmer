using Domain.DTOs;

namespace Application.DAOInterface;

public interface ISensorDao
{
    Task<IEnumerable<SensorLookupDto>> GetSensorBySensorId(int fieldId);
}