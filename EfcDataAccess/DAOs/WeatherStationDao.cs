using Application.DAOInterface;
using Domain.DTOs;
using Domain.Models;

namespace EfcDataAccess.DAOs;

public class WeatherStationDao : IWeatherStationDao
{
    public Task<WeatherStationLookupDto> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<WeatherStation> GetByFieldId(int id)
    {
        throw new NotImplementedException();
    }
}