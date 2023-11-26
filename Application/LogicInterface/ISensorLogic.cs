using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterface;

public interface ISensorLogic
{
    public Task<IEnumerable<SensorLookupDto>> GetAsync(int fieldId);
    Task<Sensor> CreateSensorAsync(SensorCreationDto dto);
}