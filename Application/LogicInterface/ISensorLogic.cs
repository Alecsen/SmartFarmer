using Domain.DTOs;

namespace Application.LogicInterface;

public interface ISensorLogic
{
    public Task<IEnumerable<SensorLookupDto>> GetAsync(int fieldId);
}