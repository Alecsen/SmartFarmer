using Application.DAOInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess.DAOs;

public class WeatherEfcStationDao : IWeatherStationDao
{
    
    private readonly SmartFarmerAppContext context;

    public WeatherEfcStationDao(SmartFarmerAppContext context)
    {
        this.context = context;
    }


    public Task<WeatherStation> CreateWeatherStationAsync(int fieldId)
    {
        WeatherStation toCreate = new WeatherStation
        {

        };
        throw new NotImplementedException();
    }

    public Task<WeatherStationLookupDto> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<WeatherStation> GetByFieldId(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<WeatherStation>> GetWeatherStations()
    {
        List<WeatherStation> stations = await context.WeatherStations.ToListAsync();
        return stations;
    }

    public async Task UpdateWeatherStations(IEnumerable<WeatherStation> updatedStations)
    {
        foreach (var station in updatedStations)
        {
            context.Entry(station).State = EntityState.Modified;
        }

        await context.SaveChangesAsync();
    }
    
}