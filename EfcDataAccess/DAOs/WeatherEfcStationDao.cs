using Application.DAOInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class WeatherEfcStationDao : IWeatherStationDao
{
    
    private readonly SmartFarmerAppContext context;

    public WeatherEfcStationDao(SmartFarmerAppContext context)
    {
        this.context = context;
    }


    public async Task<WeatherStation> CreateWeatherStationAsync(int fieldId)
    {
        WeatherStation toCreate = new WeatherStation
        {
            FieldId = fieldId,
            WindDirection = " ",
            Evaporation = 0,
            WindSpeed = 0,
            Precipitation = 0,
        };
        EntityEntry<WeatherStation> newWeatherStation = await context.WeatherStations.AddAsync(toCreate);
        await context.SaveChangesAsync();
        return newWeatherStation.Entity;
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