using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class WeatherStationLogic : IWeatherStationLogic
{
    public Task<IEnumerable<WeatherStationLookupDto>> GetAsync(int fieldId)
    {
        throw new NotImplementedException();
    }
}