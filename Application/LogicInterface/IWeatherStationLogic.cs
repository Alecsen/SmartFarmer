using Domain.DTOs;

namespace Application.LogicInterface;

public interface IWeatherStationLogic
{
    public Task<IEnumerable<WeatherStationLookupDto>> GetAsync(int fieldId);
}