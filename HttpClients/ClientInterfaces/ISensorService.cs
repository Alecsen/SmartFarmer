using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ISensorService
{
    Task<IEnumerable<SensorLookupDto>> GetSensorsByFieldId(int fieldId);
}