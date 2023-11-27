using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterface;

public interface IWeatherStationDao
{
    Task<WeatherStationLookupDto> GetById(int id);
    Task<WeatherStation> GetByFieldId(int id);
}